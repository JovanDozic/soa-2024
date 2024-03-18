using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class UserDatabaseRepository : IUserRepository
    {
        private readonly StakeholdersContext _dbContext;

        public UserDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Exists(string username)
        {
            return _dbContext.Users.Any(user => user.Username == username);
        }

        public bool ExistsByEmail(string email)
        {
            return _dbContext.Users.Any(user => user.Email == email);
        }

        public User? GetActiveByName(string username)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Username == username && user.IsActive && user.IsProfileActivated);
        }

        public User? GetActiveByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Email == email && user.IsActive && user.IsProfileActivated); 
        }

        public User Create(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        public long GetPersonId(long userId)
        {
            var person = _dbContext.People.FirstOrDefault(p => p.UserId == userId);
            if (person == null)
            {
                throw new KeyNotFoundException("Person not found.");
            }
            return person.Id;
        }

        public List<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }
        public User Update(User user)
        {
            try
            {
                _dbContext.Update(user);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return user;
        }

        public bool ActivateAccount(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            user.IsProfileActivated = true;
            _dbContext.Update(user);
            _dbContext.SaveChanges();
            return true;
        }


        public Person GetPersonById(long personId)
        {
            return _dbContext.People.FirstOrDefault(p => p.Id == personId);
        }
    }
}
