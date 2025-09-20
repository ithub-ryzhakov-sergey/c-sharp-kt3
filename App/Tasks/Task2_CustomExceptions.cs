namespace App.Tasks;

public class InvalidPasswordException : Exception
{
    public string? Password { get; private set; }
    public InvalidPasswordException(string? password, string message): base(message)
    {
        Password = password;
    }
}
public static class PasswordValidator
{
    public static void ValidatePassword(string password)
    {
        if (password == null) throw new InvalidPasswordException(password, "Пароль не может быть null");
        if (password.Length <= 7) throw new InvalidPasswordException(password, "Пароль должен иметь 8 символов");
        if (!password.Any(char.IsDigit)) throw new InvalidPasswordException(password, "Пароль должен иметь хотя бы одну цифру");
    }
}