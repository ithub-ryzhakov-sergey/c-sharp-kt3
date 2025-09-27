using System;

namespace App.Tasks
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base("Пароль не соответствует требованиям безопасности")
        {
        }

        public InvalidPasswordException(string message) : base(message)
        {
        }
    }

    public static class PasswordValidator
    {
        public static void ValidatePassword(string password)
        {
            if (password == null)
            {
                throw new InvalidPasswordException("Пароль не может быть null");
            }

            if (password.Length < 8)
            {
                throw new InvalidPasswordException("Длина пароля должна быть не менее 8 символов");
            }

            if (!ContainsDigit(password))
            {
                throw new InvalidPasswordException("Пароль должен содержать хотя бы одну цифру");
            }
        }

        private static bool ContainsDigit(string password)
        {
            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }
    }
}