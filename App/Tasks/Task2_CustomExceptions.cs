using System;

namespace App.Tasks
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base("Пароль не соответствует требованиям безопасности") { }

        public InvalidPasswordException(string message) : base(message) { }

        public InvalidPasswordException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public static class Task2_CustomExceptions
    {
        public static void ValidatePassword(string password)
        {
            if (password == null)
                throw new InvalidPasswordException("Пароль не может быть null");

            if (password.Length < 8)
                throw new InvalidPasswordException("Пароль должен содержать минимум 8 символов");

            if (!ContainsDigit(password))
                throw new InvalidPasswordException("Пароль должен содержать хотя бы одну цифру");
        }

        private static bool ContainsDigit(string input)
        {
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                    return true;
            }
            return false;
        }
    }
}