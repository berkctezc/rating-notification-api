namespace Application.Abstract;

public interface IUserProvider
{
    /// <summary>
    /// a basic function to get a user
    /// </summary>
    /// <returns></returns>
    Task<User> GetUser();
}