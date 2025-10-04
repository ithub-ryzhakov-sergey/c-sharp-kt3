using System;

namespace App.Tasks
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() { }

        public InvalidPasswordException(string message) : base(message) { }

        public InvalidPasswordException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public static class Task2_CustomExceptions
    {
        public static void ValidatePassword(string password)
        {
            if (password == null)
                throw new InvalidPasswordException("Пароль не может быть null.");

            if (password.Length < 8)
                throw new InvalidPasswordException("Пароль должен содержать не менее 8 символов.");

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
                throw new InvalidPasswordException("Пароль должен содержать хотя бы одну цифру.");
        }
    }
}