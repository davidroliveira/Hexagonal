using AutoMapper;
using Projeto.Application.Models;
using Projeto.Domain.Domains;
using System.Reflection;

namespace Projeto.Mapper;

public sealed class MappingProfile : Profile
{
    private readonly List<Type> _listDomain = Assembly
        .GetAssembly(typeof(BaseDomain))!
        .GetTypes()
        .Where(type => type.IsSealed && type.IsSubclassOf(typeof(BaseDomain)))
        .ToList();

    private readonly List<Type> _listModel = Assembly
        .GetAssembly(typeof(BaseModel))!
        .GetTypes()
        .Where(type => type.IsSealed && type.IsSubclassOf(typeof(BaseModel)))
        .ToList();

    public MappingProfile()
    {
        _listDomain.ForEach(typeDomain =>
        {
            var suffixDomain = "Domain";
            var suffixModel = "Model";
            if (!typeDomain.Name.EndsWith(suffixDomain))
                throw new Exception($"ClassDomain {typeDomain.Name} com nomenclatura sem sufixo de 'Domain'");

            var nameModel = string.Concat(typeDomain.Name[..^suffixDomain.Length], suffixModel);
            var typeModel = _listModel.FirstOrDefault(type => type.Name.Equals(nameModel));

            if (typeModel is not null)
            {
                CreateMap(typeDomain, typeModel).ReverseMap();
            }
        });
    }
}