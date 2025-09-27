using App.Models;
using App.Tasks;
using Xunit;

namespace App.Tests;

public class Task3_Tests
{
    [Fact]
    public void RegisterUser_Adult_NoException()
    {
        var service = new UserService();
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Adult",
            LastName = "User",
            Email = "adult@example.com",
            DateOfBirth = new DateTime(2000, 1, 1)
        };
        service.RegisterUser(user); // не должно выбросить
    }

    [Fact]
    public void RegisterUser_OneDayBefore14_ThrowsUnderage()
    {
        var service = new UserService();
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Almost",
            LastName = "Fourteen",
            Email = "almost14@example.com",
            DateOfBirth = new DateTime(2010, 1, 2) // возраст 13 (за день до 14)
        };
        Assert.Throws<UnderageUserException>(() => service.RegisterUser(user));
    }

    [Fact]
    public void RegisterUser_YoungUser_ThrowsUnderage()
    {
        var service = new UserService();
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Young",
            LastName = "User",
            Email = "young@example.com",
            DateOfBirth = new DateTime(2012, 5, 10) // возраст 11
        };
        Assert.Throws<UnderageUserException>(() => service.RegisterUser(user));
    }
}

