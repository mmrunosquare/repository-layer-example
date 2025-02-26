using Example.Business.Interfaces.Services;
using Example.DAL.Database;
using Example.DAL.Interfaces.Contexts;
namespace Example.Business.Services;

public class UserService(ICoreDatabase coreDatabase) : IUserService
{
    public User CreateUser(User user)
    {
        coreDatabase.User.Add(user);
        return user;
    }

    public bool DeleteUser(int id)
    {
        var user = coreDatabase.User.FirstOrDefault(x => x.Id == id);
        if (user == null)
        {
            return false;
        }
        coreDatabase.User.Remove(user);
        return true;
    }

    public User GetUser(int id)
    {
        return coreDatabase.User.FirstOrDefault(x => x.Id == id);
    }

    public List<User> GetUsers()
    {
        return coreDatabase.User.ToList();
    }

    public User UpdateUser(User user)
    {
        var existingUser = coreDatabase.User.FirstOrDefault(x => x.Id == user.Id);
        if (existingUser == null)
        {
            return null;
        }
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.Password = user.Password;
        existingUser.Role = user.Role;
        existingUser.IsActive = user.IsActive;
        existingUser.UpdatedAt = DateTime.Now;
        return existingUser;
    }
}
