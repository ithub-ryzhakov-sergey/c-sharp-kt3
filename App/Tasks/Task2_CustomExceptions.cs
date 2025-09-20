using System;

namespace App.Tasks
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base("������ �� ������������� ����������� ������������") { }

        public InvalidPasswordException(string message) : base(message) { }

        public InvalidPasswordException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public static class Task2_CustomExceptions
    {
        public static void ValidatePassword(string password)
        {
            if (password == null)
                throw new InvalidPasswordException("������ �� ����� ���� null");

            if (password.Length < 8)
                throw new InvalidPasswordException("������ ������ ��������� ������� 8 ��������");

            if (!ContainsDigit(password))
                throw new InvalidPasswordException("������ ������ ��������� ���� �� ���� �����");
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