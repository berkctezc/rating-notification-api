namespace Domain.Entities;

[ExcludeFromCodeCoverage]
public class User : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}