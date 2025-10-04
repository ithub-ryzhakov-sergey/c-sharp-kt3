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
                throw new InvalidPasswordException("������ �� ����� ���� null.");

            if (password.Length < 8)
                throw new InvalidPasswordException("������ ������ ��������� �� ����� 8 ��������.");

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
                throw new InvalidPasswordException("������ ������ ��������� ���� �� ���� �����.");
        }
    }
}