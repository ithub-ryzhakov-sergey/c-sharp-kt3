namespace App.Tasks;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() : base() { }

    public InvalidPasswordException(string message) : base(message) { }

    public InvalidPasswordException(string message, Exception innerException)
        : base(message, innerException) { }
}

public static class PasswordValidator
{

    public static void ValidatePassword(string password)
    {
        if (password == null)
            throw new InvalidPasswordException("Не может быть null");
        if (password.Length < 8)
            throw new InvalidPasswordException("Не может быть меньше 8");
        if (!password.Any(char.IsDigit))
            throw new InvalidPasswordException("Не может быть цифр");
    }
}



