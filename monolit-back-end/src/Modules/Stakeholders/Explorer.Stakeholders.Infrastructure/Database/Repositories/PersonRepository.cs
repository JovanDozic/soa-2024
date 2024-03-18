using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class PersonRepository: IPersonRepository
    {
        private readonly StakeholdersContext _dbContext;

        public PersonRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Person> GetAll(List<string> usernames)
        {
            var lowercasedUsernames = usernames.Select(username => username.ToLower());
            var userIds = _dbContext.Users.AsEnumerable()
                .Where(u => lowercasedUsernames.Contains(u.Username.ToLower()))
                .Select(user => user.Id)
                .ToList();
            return _dbContext.People.ToList()
                .Where(p => userIds.Contains(p.UserId))
                .ToList();
        }
        public Person Update(Person person)
        {
            try
            {
                _dbContext.Update(person);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return person;
        }
    }
}