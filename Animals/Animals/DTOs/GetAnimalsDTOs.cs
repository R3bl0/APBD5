namespace Animals.DTOs
{
    public record GetAnimalsResponse(int id, string Name, string Description, string Category, string Area);
}