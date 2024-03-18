namespace Explorer.Stakeholders.API.Dtos;

public class PersonDto
{
    public int Id { get; set; }
    public long UserId { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Picture { get; init; }
    public string Bio { get; init; }
    public string Quote { get; init; }
    public int Xp { get; init; }
    public int Level { get; init; }

}