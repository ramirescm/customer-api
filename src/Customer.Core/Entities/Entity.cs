namespace Customer.Core.Entities;

public class Entity
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}