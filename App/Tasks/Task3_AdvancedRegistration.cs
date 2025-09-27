using App.Models;
using Bogus;

namespace App.Tasks;

public class UserRegistrationException : Exception
{
    protected UserRegistrationException() { } // Чтобы UnderageUserException не жаловался и в задании сказано ток про 2 конструктора => без параметров для детей :)
    public UserRegistrationException(string message) { }

    public UserRegistrationException(string message, Exception inner) { }
}

public class UnderageUserException : UserRegistrationException
{
    public UnderageUserException() { }
}
public class UserService
{
    public List<User> GenerateUsers(int count)
    {
        Faker<User> faker = new Faker<User>("ru")
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.DateOfBirth, f => f.Date.Past());

        return faker.Generate(count);
    }

    public void RegisterUser(User user)
    {
        Faker faker = new Faker();
        TimeSpan ageCheck = faker.Date.Future() - user.DateOfBirth; // Тест за "один день до" не проходит, т.к. от текущей даты всё норм, нужна фейковая текущая дата, а как её сделать я не понял :( 
        if (ageCheck.TotalDays / 365 < 14)
        {
            throw new UnderageUserException();
        } 
    }
}

