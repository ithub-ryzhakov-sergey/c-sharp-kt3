using System;
using System.Collections.Generic;
using App.Models;
using Bogus;

namespace App.Tasks
{
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException(string message) : base(message) { }
        public UserRegistrationException(string message, Exception inner) : base(message, inner) { }
    }

    public class UnderageUserException : UserRegistrationException
    {
        public int Age { get; }

        public UnderageUserException(int age, string message) : base(message)
        {
            Age = age;
        }
    }

    public class UserService
    {
        private static readonly DateTime CurrentDate = new DateTime(2024, 1, 1);
        private readonly Faker<User> _userFaker;

        public UserService()
        {
            _userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => f.Random.Guid())
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.DateOfBirth, f => f.Date.Past(80, DateTime.Now.AddYears(-5)));
        }

        public List<User> GenerateUsers(int count)
        {
            try
            {
                return _userFaker.Generate(count);
            }
            catch (Exception ex)
            {
                throw new UserRegistrationException("Ошибка генерации пользователей", ex);
            }
        }

        public void RegisterUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            try
            {
                ValidateAge(user.DateOfBirth);
            }
            catch (UnderageUserException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UserRegistrationException($"Ошибка регистрации пользователя {user.Email}", ex);
            }
        }

        private void ValidateAge(DateTime birthDate)
        {
            int age = CalculateAge(birthDate);

            if (age < 14)
            {
                throw new UnderageUserException(
                    age,
                    $"Пользователь несовершеннолетний. Возраст: {age}, минимальный: 14. Дата рождения: {birthDate:дд--мм--гггг}"
                );
            }
        }

        private int CalculateAge(DateTime birthDate)
        {
            int age = CurrentDate.Year - birthDate.Year;

            if (birthDate > CurrentDate.AddYears(-age))
                age--;

            return age;
        }
    }
}