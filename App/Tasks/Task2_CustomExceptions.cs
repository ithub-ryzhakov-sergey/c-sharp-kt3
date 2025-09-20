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
        if (password == null) throw new InvalidPasswordException(password, "������ �� ����� ���� null");
        if (password.Length <= 7) throw new InvalidPasswordException(password, "������ ������ ����� 8 ��������");
        if (!password.Any(char.IsDigit)) throw new InvalidPasswordException(password, "������ ������ ����� ���� �� ���� �����");
    }
}