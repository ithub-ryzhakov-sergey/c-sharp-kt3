using System;

namespace App.Tasks
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base("������ �� ������������� ����������� ������������")
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
                throw new InvalidPasswordException("������ �� ����� ���� null");
            }

            if (password.Length < 8)
            {
                throw new InvalidPasswordException("����� ������ ������ ���� �� ����� 8 ��������");
            }

            if (!ContainsDigit(password))
            {
                throw new InvalidPasswordException("������ ������ ��������� ���� �� ���� �����");
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