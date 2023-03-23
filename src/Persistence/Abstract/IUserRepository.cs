namespace Persistence.Abstract;

public interface IUserRepository
{
    Task<User> GetFirstUser();
}