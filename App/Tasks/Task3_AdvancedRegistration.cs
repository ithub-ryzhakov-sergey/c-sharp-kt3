using System;
using System.Collections.Generic;
using Bogus;
using App.Models;

namespace App.Tasks
{
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException(string message) : base(message) { }
        public UserRegistrationException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class UnderageUserException : UserRegistrationException
    {
        public UnderageUserException(string message) : base(message) { }
        public UnderageUserException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class UserService
    {
        private readonly DateTime _currentDate = new DateTime(2024, 1, 1);

        public List<User> GenerateUsers(int count)
        {
            try
            {
                var userFaker = new Faker<User>()
                    .RuleFor(u => u.Id, f => Guid.NewGuid())
                    .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                    .RuleFor(u => u.LastName, f => f.Name.LastName())
                    .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                    .RuleFor(u => u.DateOfBirth, f => f.Date.Past(80, new DateTime(2024, 1, 1).AddYears(-5)));

                return userFaker.Generate(count);
            }
            catch (Exception ex)
            {
                throw new UserRegistrationException("Ошибка при генерации пользователей", ex);
            }
        }

        public void RegisterUser(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                if (user.DateOfBirth == default(DateTime))
                {
                    throw new ArgumentException("Дата рождения не указана");
                }

                int age = _currentDate.Year - user.DateOfBirth.Year;

                if (user.DateOfBirth.Date > _currentDate.AddYears(-age))
                {
                    age--;
                }

                if (age < 14)
                {
                    throw new UnderageUserException($"Пользователю всего {age} лет. Минимальный возраст: 14 лет.");
                }
            }
            catch (UnderageUserException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UserRegistrationException("Ошибка при регистрации пользователя", ex);
            }
        }
    }
}