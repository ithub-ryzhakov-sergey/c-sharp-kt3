using App.Models;
using Bogus;

namespace App.Tasks;

public class UserRegistrationException : Exception
{
    public UserRegistrationException(string message, Exception inner = null) : base(message, inner) { }
}

public class UnderageUserException : UserRegistrationException
{
    public int Age { get; }
    public UnderageUserException(int age, string message) : base(message) => Age = age;
}

public class UserService
{
    private static readonly DateTime CurrentDate = new DateTime(2024, 1, 1);

    public List<User> GenerateUsers(int count)
    {
        var faker = new Faker<User>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.FirstName, f => f.Person.FirstName)
            .RuleFor(u => u.LastName, f => f.Person.LastName)
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.DateOfBirth, f => f.Date.Past(80, DateTime.Now.AddYears(-5)));

        return faker.Generate(count);
    }

    public void RegisterUser(User user)
    {
        try
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (user.DateOfBirth == default) throw new ArgumentException("Invalid date of birth");

            int age = CurrentDate.Year - user.DateOfBirth.Year;
            if (user.DateOfBirth > CurrentDate.AddYears(-age)) age--;

            if (age < 14) throw new UnderageUserException(age, $"User is only {age} years old");
        }
        catch (UnderageUserException) { throw; }
        catch (Exception ex) { throw new UserRegistrationException("Registration failed", ex); }
    }
}