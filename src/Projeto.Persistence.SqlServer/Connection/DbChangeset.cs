﻿using Dapper;
using Projeto.Base.Common.Helpers;
using Projeto.Domain.Connection;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Projeto.Persistence.SqlServer.Connection;

public sealed class DbChangeset : IDbChangeset
{
    private readonly IDbConnection _connection;
    private readonly Dictionary<(string, string), long> _cacheChangeSets = new();

    public DbChangeset(IDbSettings settings) => _connection = new SqlConnection(settings.ConnectionString);

    public void Apply()
    {
        _cacheChangeSets.Clear();

        if (!ExistsDatabase())
            CreateDatabase();

        ApplyChangesetsDatabase();
    }

    private bool ExistsTableChangesets()
    {
        var result = _connection.QuerySingle<long>(@"
          select count(1) 
            from information_schema.tables 
           where table_name = @table_name
             and table_catalog = @table_catalog",
            new Dictionary<string, object>
            {
                { "@table_name", "changesets" },
                { "@table_catalog", _connection.Database }
            });
        return result > 0;
    }

    private void FillCacheChangeSets()
    {
        var retorno = _connection.Query<(long codigoLocal, string checksum, string fileName)>(@"
          select codigo_local codigoLocal,
                 checksum Checksum, 
                 file_name fileName
            from changesets").ToList();
        _cacheChangeSets.Clear();
        retorno.ForEach(row => _cacheChangeSets.Add((row.fileName, row.checksum), row.codigoLocal));
        _cacheChangeSets.TrimExcess();
    }

    private bool ScriptJaExecutado(string changesetFile, string conteudo)
    {
        if (!ExistsTableChangesets())
            return false;

        var fileName = Path.GetFileName(changesetFile);

        if (!_cacheChangeSets.Values.ToList().Any())
            FillCacheChangeSets();

        return _cacheChangeSets.ContainsKey((fileName, conteudo.CheckSum()));
    }

    private void GravarScriptComoExecutado(string changesetFile, string checksum)
    {
        var fileName = Path.GetFileName(changesetFile);

        var idLocal = _connection.QuerySingle<long>(@"
            with cte
              as (select @file_name file_name,
                         @checksum checksum)
            merge 
              changesets as Destino
            using 
              cte AS Origem on (Origem.file_name = Destino.file_name)
            when matched then
                 update set Destino.file_name = Origem.file_name, 
                            Destino.checksum = Origem.checksum
            when not matched then
                insert (file_name, 
                        checksum)
                values (Origem.file_name, 
                        Origem.checksum)
            output inserted.codigo_local;",
                new Dictionary<string, object>
                {
                    { "@file_name", fileName },
                    { "@checksum", checksum }
                });

        _cacheChangeSets.Add((fileName, checksum), idLocal);
        _cacheChangeSets.TrimExcess();
    }

    private void ApplyChangesetsDatabase()
    {
        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss:ffff} start ApplyChangesetsDatabase");

        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var filePathRelativeToAssembly = $"{assemblyPath}\\Changesets";
        var normalizedPath = Path.GetFullPath(filePathRelativeToAssembly);
        foreach (var changesetFile in Directory.GetFiles(normalizedPath))
        {
            Console.WriteLine();
            Console.Write($"{DateTime.Now:yyyy-MM-dd HH:mm:ss:ffff} \"{changesetFile}\"");
            var text = File.ReadAllText(changesetFile);

            if (ScriptJaExecutado(changesetFile, text))
            {
                Console.Write(" ready");
                continue;
            }

            Console.Write(" executed ");
            Regex regex = new(@"\b(?i)GO(?-i)\b");
            var parts = regex.Split(text.Trim()).ToList();

            parts.Where(part => !string.IsNullOrEmpty(part.Trim()))
                .ToList()
                .ForEach(part => _connection.Execute(part));

            GravarScriptComoExecutado(changesetFile, text.CheckSum());
        }
        Console.WriteLine();
        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss:ffff} end ApplyChangesetsDatabase");
    }

    private bool ExistsDatabase()
    {
        var builder = new SqlConnectionStringBuilder(_connection.ConnectionString);
        var initialCatalogOriginal = _connection.Database;
        builder.InitialCatalog = string.Empty;

        _connection.ConnectionString = builder.ToString();
        var result = _connection.QuerySingle<long>(@"
          select count(1) 
            from sys.databases 
           where name = @database_name",
            new Dictionary<string, object> { { "@database_name", initialCatalogOriginal } });

        builder.InitialCatalog = initialCatalogOriginal;
        _connection.ConnectionString = builder.ToString();

        return result > 0;
    }

    private void CreateDatabase()
    {
        var initialCatalogOriginal = _connection.Database;
        var connectionStringBuilder = new SqlConnectionStringBuilder(_connection.ConnectionString)
        {
            InitialCatalog = string.Empty
        };

        _connection.ConnectionString = connectionStringBuilder.ToString();
        _connection.Execute(@$"
            if not exists(select 1 from sys.databases where name = '{initialCatalogOriginal}')
            begin
              create database [{initialCatalogOriginal}]
            end");

        connectionStringBuilder.InitialCatalog = initialCatalogOriginal;
        _connection.ConnectionString = connectionStringBuilder.ToString();
    }

    public void Dispose() => _connection.Dispose();
}