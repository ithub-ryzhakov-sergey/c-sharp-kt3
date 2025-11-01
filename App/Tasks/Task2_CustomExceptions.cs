using static System.Net.Mime.MediaTypeNames;

namespace App.Tasks;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException(string message) : base(message) { }
}

public static class PasswordValidator
{
    public static void ValidatePassword(string password)
    {
        if (password == null)
            throw new InvalidPasswordException("Пароль не может быть null.");

        if (password.Length < 8)
            throw new InvalidPasswordException("Пароль должен содержать минимум 8 символов.");

        if (!password.Any(char.IsDigit))
            throw new InvalidPasswordException("Пароль должен содержать хотя бы одну цифру.");
    }
}