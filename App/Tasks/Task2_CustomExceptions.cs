using System.Security.Cryptography.X509Certificates;

namespace App.Tasks;

public static class PasswordValidator
{
    public static void ValidatePassword(string password)
    {
        if (password == null) throw new InvalidPasswordException("������ �� ����� ���� ����");
        if (password.Length < 8) throw new InvalidPasswordException("������ ������� ��������");
        if (password.Length < 16) throw new InvalidPasswordException("������ ���������");
        bool hasDigit = false;
        foreach (char c in password)
            if (char.IsDigit(c)) { hasDigit = true; break; }

        if (!hasDigit) throw new InvalidPasswordException("����� �� �������");
    }
}

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() { }
    public InvalidPasswordException(string message) : base(message) { }
    public InvalidPasswordException(string message, Exception inner) : base(message, inner) { }
}