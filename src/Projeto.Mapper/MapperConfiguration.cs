using AutoMapper;

namespace Projeto.Mapper;

public sealed class MapperConfiguration
{
    private readonly AutoMapper.MapperConfiguration _mapperConfiguration;

    public MapperConfiguration()
    {
        _mapperConfiguration = new AutoMapper.MapperConfiguration(configure =>
        {
            configure.AddProfile(new MappingProfile());
        });
    }

    public IMapper CreateMapper() => _mapperConfiguration.CreateMapper();
}