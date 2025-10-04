namespace App.Tasks;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() { }
}


public static class PasswordValidator
{
    public static void ValidatePassword(string password)
    {
        if (String.IsNullOrEmpty(password) || password.Length < 8 || (!password.Any(char.IsDigit)))
        {
            throw new InvalidPasswordException();
        }

    }
}

