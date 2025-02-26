using Example.DAL.Database;

namespace Example.Business.Interfaces.Services;
public interface IUserService
{
    public List<User> GetUsers();
    public User GetUser(int id);
    public User CreateUser(User user);
    public User UpdateUser(User user);
    public bool DeleteUser(int id);
}
