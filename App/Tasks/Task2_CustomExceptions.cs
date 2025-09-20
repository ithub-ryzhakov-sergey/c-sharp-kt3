using System;

namespace App.Tasks
{
    // Создаем собственное исключение
    public class InvalidPasswordException : Exception
    {
        // Три стандартных конструктора
        public InvalidPasswordException() : base() { }

        public InvalidPasswordException(string message) : base(message) { }

        public InvalidPasswordException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public static class PasswordValidator
    {
        public static void ValidatePassword(string password)
        {
            // Проверка на null
            if (password == null)
            {
                throw new InvalidPasswordException("Password cannot be null");
            }

            // Проверка длины
            if (password.Length < 8)
            {
                throw new InvalidPasswordException("Password must be at least 8 characters long");
            }

            // Проверка наличия цифры
            bool hasDigit = false;
            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    hasDigit = true;
                    break;
                }
            }

            if (!hasDigit)
            {
                throw new InvalidPasswordException("Password must contain at least one digit");
            }
        }
    }
}

