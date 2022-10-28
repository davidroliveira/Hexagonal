using AutoMapper;
using Projeto.Application.Base;
using Projeto.Application.Models;
using Projeto.Domain.Repositories;

namespace Projeto.Application.UseCases.WeatherForecast;

public sealed class WeatherForecastUseCase : BaseUseCase<WeatherForecastRequest, WeatherForecastResponse>
{
    private readonly IMapper _mapper;
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public WeatherForecastUseCase(IMapper mapper, IWeatherForecastRepository weatherForecastRepository)
    {
        _mapper = mapper;
        _weatherForecastRepository = weatherForecastRepository;
    }

    public override Task<WeatherForecastResponse> Execute(WeatherForecastRequest request) => Task.FromResult(new WeatherForecastResponse(Task.FromResult(_weatherForecastRepository.Listar().GetAwaiter().GetResult().Select(domain => _mapper.Map<WeatherForecastModel>(domain)))));
}