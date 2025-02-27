using Example.Business.Interfaces.Services;
using Example.DAL.Database;
using Example.DAL.Interfaces.Contexts;
using Example.Repository.Interfaces;
namespace Example.Business.Services;

public class UserService(IUserRepository repository) : IUserService
{
    public User CreateUser(User user)
    {
        var newUser = repository.CreateUser(user);
        return user;
    }

    public bool DeleteUser(int id)
    {
        return repository.DeleteUser(id);
    }

    public User GetUser(int id)
    {
        return repository.GetUser(id);
    }

    public List<User> GetUsers()
    {
        return repository.GetUser();
    }

    public User UpdateUser(User user)
    {
        var updatedUser = repository.UpdateUser(user);
        return updatedUser;
    }
}
