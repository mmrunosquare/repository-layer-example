using Example.DAL.Database;
using Example.DAL.Interfaces.Contexts;
using Example.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Repository.Repository;

public class UserRepository : IUserRepository
{
    private ICoreDatabase CoreDatabase { get; }

    public UserRepository(ICoreDatabase coreDatabase)
    {
        CoreDatabase = coreDatabase;
    }

    public User CreateUser(User example)
    {
        CoreDatabase.User.Add(example);
        return example;
    }

    public bool DeleteUser(int id)
    {
        var example = CoreDatabase.User.FirstOrDefault(x => x.Id == id);
        if (example == null)
        {
            return false;
        }
        CoreDatabase.User.Remove(example);
        return true;
    }

    public User GetUser(int id)
    {
        return CoreDatabase.User.FirstOrDefault(x => x.Id == id);
    }

    public List<User> GetUser()
    {
        return CoreDatabase.User.ToList();
    }

    public User UpdateUser(User example)
    {
        var existingExample = CoreDatabase.User.FirstOrDefault(x => x.Id == example.Id);
        if (existingExample == null)
        {
            return null;
        }
        existingExample.Name = example.Name;
        existingExample.Email = example.Email;
        existingExample.Password = example.Password;
        existingExample.Role = example.Role;
        existingExample.IsActive = example.IsActive;
        existingExample.UpdatedAt = DateTime.Now;
        return existingExample;
    }
}
