namespace Application;

[ExcludeFromCodeCoverage]
public static class DependencyContainer
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services
            .AddMapper()
            .AddFluentValidationValidators()
            .AddScoped<IRatingService, RatingService>()
            .AddScoped<IUserProvider, FakeUserProvider>();

        return services;
    }

    private static IServiceCollection AddMapper(this IServiceCollection services)
        => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    private static IServiceCollection AddFluentValidationValidators(this IServiceCollection services)
        => services
            .AddValidatorsFromAssemblyContaining<GetAverageRatingOfProviderRequestModelValidator>();
}