using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace App.Tasks
{
    // ������� ���������� ����������� ������������
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException(string message) : base(message) { }

        public UserRegistrationException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    // ����������� ����������: ������������ ������������������
    public class UnderageUserException : UserRegistrationException
    {
        public UnderageUserException(string message) : base(message) { }

        public UnderageUserException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    // ������ ������������
    public class User
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }

    // ������ ����������� �������������
    public static class UserService
    {
        // ������������� "�������" ����
        private static readonly DateTime CurrentDate = new DateTime(2024, 1, 1);

        // ��������� ������������� � ������� Bogus
        public static List<User> GenerateUsers(int count)
        {
            if (count < 0)
                throw new ArgumentException("���������� ������������� �� ����� ���� �������������.");

            var userFaker = new Faker<User>()
                .RuleFor(u => u.Name, f => f.Person.FullName)
                .RuleFor(u => u.BirthDate, f => f.Date.Past(50, CurrentDate)); // �������� �� ��������� 50 ���

            return userFaker.Generate(count);
        }

        // ����������� ������������
        public static void RegisterUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "������������ �� ����� ���� null.");

                // ���������� �������� �� ������������� ����
                int age = CurrentDate.Year - user.BirthDate.Year;

                // �������������, ���� ���� �������� ��� �� �������� � ������� ����
                if (user.BirthDate > CurrentDate.AddYears(-age))
                    age--;

                if (age < 14)
                {
                    throw new UnderageUserException($"������������ {user.Name} ������������������. �������: {age} ���.");
                }

                // ����� ����� ���� ������ ���������� � ��, �������� email � �.�.
                // ��� ������������ � ������ �������� ����������.
            }
            catch (UnderageUserException)
            {
                // ���������� � ��� ����������� ����������, ��� �� �����������
                throw;
            }
            catch (Exception ex) when (!(ex is UnderageUserException))
            {
                // ����������� ����� ������ ���������� � UserRegistrationException
                throw new UserRegistrationException("������ ��� ����������� ������������.", ex);
            }
        }
    }
}