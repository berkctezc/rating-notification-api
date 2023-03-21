namespace Application.Abstract;

public interface IUserProvider
{
    Task<User> GetUser();
}