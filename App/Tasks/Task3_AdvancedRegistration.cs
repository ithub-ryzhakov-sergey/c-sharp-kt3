using App.Models;
using Bogus;
using System;
using System.Collections.Generic;

namespace App.Tasks;

public class UserRegistrationException : Exception
{
    public UserRegistrationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

public class UnderageUserException : UserRegistrationException
{
    public UnderageUserException(string message, Exception innerException = null)
        : base(message, innerException)
    {
    }
}

public class UserService
{
    private static readonly DateTime FixedCurrentDate = new DateTime(2024, 1, 1);

    public List<User> GenerateUsers(int count)
    {
        var userFaker = new Faker<User>()
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.DateOfBirth, f => f.Date.Past(30, FixedCurrentDate.AddYears(-14))); // Ensure users are at least 14 years old on FixedCurrentDate

        return userFaker.Generate(count);
    }

    public void RegisterUser(User user)
    {
        try
        {
            // Simulate potential registration errors (e.g., database issues)
            if (user.Email.Contains("example"))
                throw new Exception("Database error occurred");

            int age = CalculateAge(user.DateOfBirth);
            if (age < 14)
                throw new UnderageUserException($"User is underage. Age: {age}");
        }
        catch (UnderageUserException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new UserRegistrationException("Error during user registration", ex);
        }
    }

    private int CalculateAge(DateTime dateOfBirth)
    {
        int age = FixedCurrentDate.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > FixedCurrentDate.AddYears(-age)) age--;
        return age;
    }
}
