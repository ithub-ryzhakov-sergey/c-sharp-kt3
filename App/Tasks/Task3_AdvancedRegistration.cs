using System;
using System.Collections.Generic;
using App.Models;
using Bogus;

namespace App.Tasks
{
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException(string message) : base(message) { }
        public UserRegistrationException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class UnderageUserException : UserRegistrationException
    {
        public UnderageUserException(string message) : base(message) { }
        public UnderageUserException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public static class UserService
    {
        private static readonly DateTime FixedCurrentDate = new DateTime(2024, 1, 1);

        public static List<User> GenerateUsers(int count)
        {
            var faker = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-5)));

            return faker.Generate(count);
        }

        public static void RegisterUser(User user)
        {
            try
            {
                int age = user.GetAge(FixedCurrentDate);

                if (age < 14)
                {
                    throw new UnderageUserException(
                        $"������������ {user.FirstName} {user.LastName} ������� �����. �������: {age} ���. ����������� �������: 14 ���.");
                }

                // �������������� ������ �����������...
                Console.WriteLine($"������������ {user.FirstName} {user.LastName} ������� ���������������.");
            }
            catch (UnderageUserException)
            {
                throw; // ������������ ����������� ���������� ������
            }
            catch (Exception ex)
            {
                throw new UserRegistrationException(
                    $"������ ��� ����������� ������������ {user.FirstName} {user.LastName}", ex);
            }
        }
    }
}