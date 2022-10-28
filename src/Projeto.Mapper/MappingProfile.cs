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
    }
}