using System;
using System.Collections.Generic;
using App.Models;

namespace App.Tasks;

public class UserRegistrationException : Exception
{
    public UserRegistrationException(string message, Exception innerException)
    : base(message, innerException) { }
}

public class UnderageUserException : UserRegistrationException
{
    public UnderageUserException(string message)
    : base(message, null) { }
}

public class UserService
{
    private readonly DateTime _currentDate = new DateTime(2024, 1, 1);

    public List<User> GenerateUsers(int count)
    {
        return [];
    }

    public void RegisterUser(User user)
    {
        try
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var age = _currentDate.Year - user.DateOfBirth.Year;
            if (user.DateOfBirth.Date > _currentDate.AddYears(-age))
                age--;

            if (age < 14)
                throw new UnderageUserException($"Пользователь должен быть старше 14 лет. Текущий возраст: {age}");
        }
        catch (UnderageUserException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new UserRegistrationException("Ошибка регистрации пользователя", ex);
        }
    }
}