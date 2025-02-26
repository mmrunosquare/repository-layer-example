using Example.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace Example.DAL.Interfaces.Contexts;

public interface ICoreDatabase    
{
    List<User> User { get; set; }
}
