using System;
using System.Collections.Generic;
using Bogus;
using App.Models;

namespace App.Tasks
{
    // Базовое исключение для регистрации пользователей
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException(string message) : base(message) { }
        public UserRegistrationException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    // Исключение для несовершеннолетних пользователей
    public class UnderageUserException : UserRegistrationException
    {
        public UnderageUserException(string message) : base(message) { }
        public UnderageUserException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class UserService
    {
        private readonly DateTime _currentDate = new DateTime(2024, 1, 1);
        private readonly Faker<User> _userFaker;

        public UserService()
        {
            // Инициализация Bogus для генерации случайных пользователей
            _userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
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
                // Оборачиваем любые исключения при генерации в UserRegistrationException
                throw new UserRegistrationException("Failed to generate users", ex);
            }
        }

        public void RegisterUser(User user)
        {
            try
            {
                // Проверяем, что пользователь не null
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User cannot be null");
                }

                // Проверяем дату рождения
                if (user.DateOfBirth == default)
                {
                    throw new ArgumentException("Date of birth is required", nameof(user));
                }

                // Вычисляем возраст относительно фиксированной даты
                int age = CalculateAge(user.DateOfBirth);

                // Проверяем возраст
                if (age < 14)
                {
                    throw new UnderageUserException($"User is underage. Age: {age}, minimum required: 14");
                }

                // Здесь могла бы быть дополнительная логика регистрации
                // Например, сохранение в базу данных и т.д.
            }
            catch (UnderageUserException)
            {
                // Перебрасываем UnderageUserException без оборачивания
                throw;
            }
            catch (Exception ex)
            {
                // Оборачиваем все другие исключения в UserRegistrationException
                throw new UserRegistrationException("Failed to register user", ex);
            }
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            try
            {
                int age = _currentDate.Year - dateOfBirth.Year;

                // Если день рождения еще не наступил в текущем году, вычитаем 1 год
                if (dateOfBirth.Date > _currentDate.AddYears(-age))
                {
                    age--;
                }

                return age;
            }
            catch (Exception ex)
            {
                // Оборачиваем ошибки вычисления возраста
                throw new UserRegistrationException("Failed to calculate age", ex);
            }
        }
    }
}