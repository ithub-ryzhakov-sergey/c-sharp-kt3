using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace App.Tasks
{
    // Базовое исключение регистрации пользователя
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException(string message) : base(message) { }

        public UserRegistrationException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    // Специфичное исключение: пользователь несовершеннолетний
    public class UnderageUserException : UserRegistrationException
    {
        public UnderageUserException(string message) : base(message) { }

        public UnderageUserException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    // Модель пользователя
    public class User
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }

    // Сервис регистрации пользователей
    public static class UserService
    {
        // Фиксированная "текущая" дата
        private static readonly DateTime CurrentDate = new DateTime(2024, 1, 1);

        // Генерация пользователей с помощью Bogus
        public static List<User> GenerateUsers(int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество пользователей не может быть отрицательным.");

            var userFaker = new Faker<User>()
                .RuleFor(u => u.Name, f => f.Person.FullName)
                .RuleFor(u => u.BirthDate, f => f.Date.Past(50, CurrentDate)); // Рождённые за последние 50 лет

            return userFaker.Generate(count);
        }

        // Регистрация пользователя
        public static void RegisterUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "Пользователь не может быть null.");

                // Вычисление возраста на фиксированную дату
                int age = CurrentDate.Year - user.BirthDate.Year;

                // Корректировка, если день рождения ещё не наступил в текущем году
                if (user.BirthDate > CurrentDate.AddYears(-age))
                    age--;

                if (age < 14)
                {
                    throw new UnderageUserException($"Пользователь {user.Name} несовершеннолетний. Возраст: {age} лет.");
                }

                // Здесь может быть логика сохранения в БД, отправки email и т.п.
                // Для демонстрации — просто успешное завершение.
            }
            catch (UnderageUserException)
            {
                // Пропускаем — это специфичное исключение, его не оборачиваем
                throw;
            }
            catch (Exception ex) when (!(ex is UnderageUserException))
            {
                // Оборачиваем любое другое исключение в UserRegistrationException
                throw new UserRegistrationException("Ошибка при регистрации пользователя.", ex);
            }
        }
    }
}