using App.Models;
using System;
using System.Collections.Generic;
using Bogus;

namespace App.Tasks
{
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException(string message, Exception innerException = null)
            : base(message, innerException)
        {

        }
    }






    public class UnderageUserException : UserRegistrationException
    {
        public UnderageUserException(string message, Exception innerException = null)
            : base(message, innerException) { }
    }



    public class UserService
    {
        private static readonly DateTime CurrentDate = new DateTime(2024, 1, 1);

        public List<User> GenerateUsers(int count)
        {
            var f1 = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.DateOfBirth, f => f.Date.Past(60, CurrentDate));

            return f1.Generate(count);
        }

        public void RegisterUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User not null");

                int age = CalculateAge(user.DateOfBirth, CurrentDate);

                if (age < 14)
                    throw new UnderageUserException("пользователь младше 14 лет");

            }
            catch (UserRegistrationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UserRegistrationException("oшибка регистрации", ex);
            }
        }

        private int CalculateAge(DateTime dob, DateTime referenceDate)
        {
            int age = referenceDate.Year - dob.Year;
            if (dob > referenceDate.AddYears(-age))
                age--;
            return age;
        }
    }
}