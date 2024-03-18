using System.ComponentModel.DataAnnotations.Schema;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class Person : Entity
{
    public long UserId { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string? Picture { get; init; }
    public string? Bio {  get; init; }
    public string? Quote { get; init; }
    public int Xp { get; private set; }
    public int Level { get; private set; } = 1;
    [NotMapped] 
    public bool CanMakeEncounter => Level >= 10;

    public Person() {}
    public Person(long userId, string name, string surname, string picture, string bio, string quote)
    {
        UserId = userId;
        Name = name;
        Surname = surname;
        Picture = picture;
        Bio = bio;
        Quote = quote;
        Validate();

    }
    public Person(long userId, string name, string surname, string? picture, string? bio, string? quote, int xp, int level)
    {
        UserId = userId;
        Name = name;
        Surname = surname;
        Picture = picture;
        Bio = bio;
        Quote = quote;
        Xp = xp;
        Level = level;
    }

    public void GainXP(int xp)
    {
        if(xp < 0) throw new ArgumentOutOfRangeException("Exception! XP must be greater than zero!");
        Xp += xp;
        Level = (int)Math.Ceiling(Math.Pow(2, (double) Xp / 100));
    }
    private void Validate()
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId");
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Surname)) throw new ArgumentException("Invalid Surname");
    }
}