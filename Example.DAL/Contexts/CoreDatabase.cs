using Example.DAL.Database;
using Example.DAL.Interfaces.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.DAL.Contexts;

public class CoreDatabase : ICoreDatabase
{
    public List<User> User { get; set; }

    public CoreDatabase()
    {
        var FakeUsers = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "John Doe",
                Email = "example1@email.com",
                Password = "password",
                Role = "Admin",
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        }.AsQueryable();

        User = [.. FakeUsers];
    }
}
