using System;

namespace App.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        public User(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public int GetAge(DateTime referenceDate)
        {
            int age = referenceDate.Year - DateOfBirth.Year;
            if (referenceDate < DateOfBirth.AddYears(age))
                age--;
            return age;
        }
    }
}