using Example.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Repository.Interfaces;    
public interface IUserRepository
{
    List<User> GetUser();
    User GetUser(int id);
    User CreateUser(User example);
    User UpdateUser(User example);
    bool DeleteUser(int id);
}   
