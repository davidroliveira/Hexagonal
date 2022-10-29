using AutoMapper;
using Projeto.Application.Models;
using Projeto.Domain.Domains;

namespace Projeto.Mapper;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WeatherForecastDomain, WeatherForecastModel>().ReverseMap();
        CreateMap<ModeloDomain, ModeloModel>().ReverseMap();
        CreateMap<PessoaDomain, PessoaModel>().ReverseMap();
        CreateMap<IEnumerable<PessoaDomain>, IEnumerable<PessoaModel>>()
            .ConvertUsing<EnumerableTypeConverter<PessoaDomain, PessoaModel>>();
    }
}

public class EnumerableTypeConverter<TSource, TDestination> : ITypeConverter<IEnumerable<TSource>, IEnumerable<TDestination>>
{
    public IEnumerable<TDestination> Convert(IEnumerable<TSource> listSources, IEnumerable<TDestination> listDestinations, ResolutionContext context) => listSources
        .Select(source => context.Mapper.Map<TSource, TDestination>(source))
        .ToList();
}

