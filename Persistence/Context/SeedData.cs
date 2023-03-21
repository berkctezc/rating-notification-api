namespace Persistence.Context;

public static class SeedData
{
    private static IEnumerable<User> GenerateUsers()
    {
        var categoryGenerator = new Faker<User>("tr")
            .RuleFor(i => i.Id, _ => Guid.NewGuid())
            .RuleFor(i => i.Name, f => f.Name.FullName());

        return categoryGenerator.Generate(100);
    }

    private static IEnumerable<ServiceProvider> GenerateServiceProviders()
    {
        var categoryGenerator = new Faker<ServiceProvider>("tr")
            .RuleFor(i => i.Id, _ => Guid.NewGuid())
            .RuleFor(i => i.Name, f => f.Name.FullName());

        return categoryGenerator.Generate(10);
    }

    private static IEnumerable<Rating> GenerateRatings(IEnumerable<Guid> userIds, IEnumerable<Guid> serviceProviderIds)
    {
        var categoryGenerator = new Faker<Rating>("tr")
            .RuleFor(i => i.Id, _ => Guid.NewGuid())
            .RuleFor(i => i.Date, f => f.Date.Between(DateTime.UtcNow.AddYears(-10), DateTime.UtcNow))
            .RuleFor(i => i.RatingNumber, f => f.Random.UInt(1u, 10u))
            .RuleFor(i => i.UserId, f => f.PickRandom(userIds))
            .RuleFor(i => i.ServiceProviderId, f => f.PickRandom(serviceProviderIds));

        return categoryGenerator.Generate(500);
    }

    public static async Task SeedAsync(string connectionString)
    {
        var dbContextBuilder = new DbContextOptionsBuilder<ArmutDbContext>();
        dbContextBuilder.UseNpgsql(connectionString);

        var context = new ArmutDbContext(dbContextBuilder.Options);

        var users = GenerateUsers().ToArray();
        var userIds = users.Select(u => u.Id);

        var serviceProviders = GenerateServiceProviders().ToArray();
        var serviceProviderIds = serviceProviders.Select(sp => sp.Id);

        var ratings = GenerateRatings(userIds, serviceProviderIds);

        await context.Users.AddRangeAsync(users);
        await context.ServiceProviders.AddRangeAsync(serviceProviders);
        await context.Ratings.AddRangeAsync(ratings);

        await context.SaveChangesAsync();
    }
}