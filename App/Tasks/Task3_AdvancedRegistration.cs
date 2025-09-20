using App.Models;
using Bogus;
namespace App.Tasks;

public class UserRegistrationException : Exception
{
    public UserRegistrationException() : base() { }

    public UserRegistrationException(string message) : base(message) { }

    public UserRegistrationException(string message, Exception innerException)
        : base(message, innerException) { }
}

public class UnderageUserException : UserRegistrationException
{
    public UnderageUserException() : base() { }

    public UnderageUserException(string message) : base(message) { }

    public UnderageUserException(string message, Exception innerException)
        : base(message, innerException) { }
}

public class UserService
{
    public List<User> GenerateUsers(int count)
    {
        return new Faker<User>().Generate(count);
    }

    public void RegisterUser(User user)
    {
        try
        {
            DateTime date1 = new DateTime(2024, 1, 1);
            TimeSpan difference = date1 - user.DateOfBirth;
            double ageInYears = difference.TotalDays / 365.25;
            if (ageInYears < 18)
                throw new UnderageUserException("Слишком мал");
        }
        catch (Exception ex)
        {
            if (ex is UnderageUserException)
                throw;

            throw new UserRegistrationException("Ошибка", ex);
        }
    }
}

