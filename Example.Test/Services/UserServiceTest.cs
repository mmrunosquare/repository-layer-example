using AutoFixture;
using Example.Business.Interfaces.Services;
using Example.Business.Services;
using Example.DAL.Database;
using Example.DAL.Interfaces.Contexts;
using Example.Repository.Interfaces;
using Moq;
using Xunit;

namespace Example.Test.Services;

public class UserServiceTest
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly IUserService _userService;
    private readonly List<User> _fakeUsers;

    public UserServiceTest()
    {
        var fixture = new Fixture();
        
        _fakeUsers = [.. fixture.CreateMany<User>(5)];

        for(int i = 0; i < _fakeUsers.Count; i++)
        {
            _fakeUsers[i].Id = i + 1;
        }

        _userRepository = new Mock<IUserRepository>();
        _userRepository.Setup(x => x.GetUser()).Returns(_fakeUsers);
        _userService = new UserService(_userRepository.Object);
    }

    [Fact]
    public void GetUsers_ShouldReturnAllUsers()
    {
        var result = _userService.GetUsers();

        Assert.NotNull(result);
        Assert.Equal(_fakeUsers.Count, result.Count);
        Assert.Equal(_fakeUsers[0].Id, result[0].Id);
        Assert.Equal(_fakeUsers[1].Name, result[1].Name);
    }
}