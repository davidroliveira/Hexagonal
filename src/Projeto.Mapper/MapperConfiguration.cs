using AutoMapper;
using AutoMapper.EquivalencyExpression;

namespace Projeto.Mapper;

public sealed class MapperConfiguration
{
    private readonly AutoMapper.MapperConfiguration _mapperConfiguration;

    public MapperConfiguration()
    {
        _mapperConfiguration = new AutoMapper.MapperConfiguration(configure =>
        {
            configure.AllowNullCollections = false;
            configure.AddCollectionMappers();
            configure.AddProfile(new MappingProfile());
        });
    }

    public IMapper CreateMapper() => _mapperConfiguration.CreateMapper();
}