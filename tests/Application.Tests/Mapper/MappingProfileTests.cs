namespace Application.Tests.Mapper;

public class MappingProfileTests
{
    private readonly MappingProfile _sut;

    public MappingProfileTests()
        => _sut = new MappingProfile();

    [Fact]
    public void Types_ShouldBeMapped()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(_sut));

        configuration.AssertConfigurationIsValid();
    }
}