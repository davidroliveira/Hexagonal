using ProjetoHexagonal.Application.UseCases.Modelo;
using ProjetoHexagonal.Commons.Application;
using NLog;
using System.ComponentModel;

namespace ProjetoHexagonal.Background;

public sealed class ModeloJob
{
    private readonly IHandler handler;
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    public ModeloJob(IHandler handler) => this.handler = handler;

    [DisplayName("Processar Job Modelo")]
    public object Run()
    {
        Logger.Info("Execução do job {Job} iniciada", GetType().Name);
        var request = new ModeloRequest();
        var result = handler.Handle(request);
        return result.Result;
    }
}
