using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class ClubDatabaseRepository : IClubRepository
{
    private readonly StakeholdersContext _dbContext;

    public ClubDatabaseRepository(StakeholdersContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(string username)
    {
        return _dbContext.Users.Any(user => user.Username == username);
    }

    public Club Create(Club club)
    {
        _dbContext.Clubs.Add(club);
        _dbContext.SaveChanges();
        return club;
    }
    public Club Update(Club club)
    {
        _dbContext.Clubs.Add(club);
        _dbContext.SaveChanges();
        return club;
    }
}
