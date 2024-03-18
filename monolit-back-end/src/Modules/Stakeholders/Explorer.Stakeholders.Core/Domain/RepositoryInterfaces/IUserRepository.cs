using Explorer.Stakeholders.Core.Domain.Users;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        bool Exists(string username);
        bool ExistsByEmail(string email);
        User? GetActiveByName(string username);
        User? GetActiveByEmail(string email);
        User Create(User user);
        long GetPersonId(long userId);
        User Update(User user);
        bool ActivateAccount(int id);
        List<User> GetAll();
        Person GetPersonById(long personId);
    }
}
