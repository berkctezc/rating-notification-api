namespace Application.Services;

[ExcludeFromCodeCoverage]
public class FakeUserProvider : IUserProvider
{
    // todo: return authenticated user
    private readonly IUserRepository _repository;

    public FakeUserProvider(IUserRepository repository)
        => _repository = repository;

    public async Task<User> GetUser()
        => await _repository.GetFirstUser();
}