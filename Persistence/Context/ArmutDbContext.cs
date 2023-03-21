namespace Persistence.Context;

public class ArmutDbContext : DbContext
{
    public ArmutDbContext()
    {
    }

    public ArmutDbContext(DbContextOptions<ArmutDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dirPath = Directory.GetCurrentDirectory();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(dirPath)
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder.UseNpgsql(configuration.GetSection("ServiceSettings:ConnectionString").Value);
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Rating> Ratings { get; set; }

    public DbSet<ServiceProvider> ServiceProviders { get; set; }
}