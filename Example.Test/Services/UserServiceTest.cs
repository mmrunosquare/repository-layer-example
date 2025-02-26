using Example.Business.Interfaces.Services;
using Example.Business.Services;
using Example.DAL.Database;
using Example.DAL.Interfaces.Contexts;
using Moq;
using Xunit;

namespace Example.Test.Services;

public class UserServiceTest
{
    private readonly Mock<ICoreDatabase> _mockDatabase;
    private readonly IUserService _userService;
    private readonly List<User> _users;

    public UserServiceTest()
    {
        _users =
        [
            new User { Id = 1, Name = "Nicholas Castro", Email = "mcdanielmatthew@thomas-smith.org", Password = "q+7bAX8k+b*Z", Role = "User", IsActive = false, CreatedAt = DateTime.Parse("2023-09-14 01:59:19"), UpdatedAt = DateTime.Parse("2024-12-02 17:07:48") },
            new User { Id = 2, Name = "Alicia Warren DDS", Email = "garrisonmichelle@white.com", Password = "Wc*S3#Jb8)x@", Role = "Moderator", IsActive = false, CreatedAt = DateTime.Parse("2023-04-04 10:31:27"), UpdatedAt = DateTime.Parse("2025-01-01 16:33:53") },
            new User { Id = 3, Name = "Hannah Harris", Email = "smithamanda@yahoo.com", Password = "&YIDKGJt@cD0", Role = "User", IsActive = false, CreatedAt = DateTime.Parse("2023-10-07 07:52:17"), UpdatedAt = DateTime.Parse("2024-04-16 23:08:10") },
            new User { Id = 4, Name = "Brandon Floyd", Email = "adixon@steele.com", Password = "E$JU@OwPjpp4", Role = "User", IsActive = false, CreatedAt = DateTime.Parse("2023-12-09 02:22:41"), UpdatedAt = DateTime.Parse("2024-07-24 01:00:09") },
            new User { Id = 5, Name = "Ashley Brewer", Email = "samanthaharrison@yahoo.com", Password = "zU2mDGb*A^K1", Role = "Guest", IsActive = false, CreatedAt = DateTime.Parse("2024-10-01 10:43:42"), UpdatedAt = DateTime.Parse("2024-05-11 23:10:22") }
        ];

        _mockDatabase = new Mock<ICoreDatabase>();

        _mockDatabase.Setup(x => x.User).Returns(_users);

        _userService = new UserService(_mockDatabase.Object);
    }

    [Fact]
    public void GetUsers_ShouldReturnAllUsers()
    {
        var result = _userService.GetUsers();

        Assert.NotNull(result);
        Assert.Equal(_users.Count, result.Count);
        Assert.Equal(_users[0].Id, result[0].Id);
        Assert.Equal(_users[1].Name, result[1].Name);
    }
}