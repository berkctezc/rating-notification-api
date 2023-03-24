namespace Persistence.Repositories;

[ExcludeFromCodeCoverage]
public class UserRepository : IUserRepository
{
    public async Task<User> GetFirstUser()
    {
        await using var context = new ArmutDbContext();

        var result = await context.Users.FirstOrDefaultAsync();

        return result;
    }
}