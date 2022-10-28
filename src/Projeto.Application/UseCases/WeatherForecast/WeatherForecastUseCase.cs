using AutoMapper;
using Projeto.Application.Base;
using Projeto.Application.Models;
using Projeto.Domain.Domains;
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

    public override async Task<WeatherForecastResponse> Execute(WeatherForecastRequest request) => await Task.FromResult(
        new WeatherForecastResponse(Task.FromResult(
            _mapper.Map<IEnumerable<WeatherForecastDomain>, IEnumerable<WeatherForecastModel>>(await _weatherForecastRepository.Listar()))));
}