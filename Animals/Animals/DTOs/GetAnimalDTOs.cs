namespace Animals.DTOs
{
    public record GetAnimalResponse(int id, string Name, string Description, string Category, string Area);
}