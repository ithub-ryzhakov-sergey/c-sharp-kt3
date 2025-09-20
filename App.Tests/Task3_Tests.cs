using Xunit;
using System;
using System.Collections.Generic;
using App.Models;
using App.Tasks;
using Bogus;

namespace App.Tests
{
    public class Task3_Tests
    {
        private readonly DateTime _fixedDate = new DateTime(2024, 1, 1);

        [Fact]
        public void UserRegistrationException_Constructors_WorkCorrectly()
        {
            // Arrange & Act
            var ex1 = new UserRegistrationException("Test message");
            var innerEx = new Exception("Inner exception");
            var ex2 = new UserRegistrationException("Test message", innerEx);

            // Assert
            Assert.NotNull(ex1);
            Assert.NotNull(ex2);
            Assert.Equal("Test message", ex1.Message);
            Assert.Equal("Test message", ex2.Message);
            Assert.Same(innerEx, ex2.InnerException);
        }

        [Fact]
        public void UnderageUserException_Constructors_WorkCorrectly()
        {
            // Arrange & Act
            var ex1 = new UnderageUserException("Test message");
            var innerEx = new Exception("Inner exception");
            var ex2 = new UnderageUserException("Test message", innerEx);

            // Assert
            Assert.NotNull(ex1);
            Assert.NotNull(ex2);
            Assert.IsType<UnderageUserException>(ex1);
            Assert.IsType<UnderageUserException>(ex2);
            Assert.IsAssignableFrom<UserRegistrationException>(ex1);
        }

        [Fact]
        public void GenerateUsers_ReturnsCorrectNumberOfUsers()
        {
            // Act
            var users = UserService.GenerateUsers(5);

            // Assert
            Assert.Equal(5, users.Count);
            Assert.All(users, user =>
            {
                Assert.NotNull(user.FirstName);
                Assert.NotNull(user.LastName);
                Assert.NotNull(user.Email);
                Assert.Contains("@", user.Email);
                Assert.True(user.DateOfBirth < DateTime.Now);
            });
        }

        [Fact]
        public void RegisterUser_AdultUser_DoesNotThrow()
        {
            // Arrange
            var user = new User("John", "Doe", "john@example.com",
                new DateTime(2000, 1, 1)); // 24 года на 2024-01-01

            // Act & Assert
            var exception = Record.Exception(() => UserService.RegisterUser(user));
            Assert.Null(exception);
        }

        [Fact]
        public void RegisterUser_UnderageUser_ThrowsUnderageUserException()
        {
            // Arrange
            var user = new User("Young", "User", "young@example.com",
                new DateTime(2015, 1, 1)); // 9 лет на 2024-01-01

            // Act & Assert
            var exception = Assert.Throws<UnderageUserException>(
                () => UserService.RegisterUser(user));

            Assert.Contains("слишком молод", exception.Message);
            Assert.Contains("9 лет", exception.Message);
        }

        [Fact]
        public void RegisterUser_Exactly14YearsOld_DoesNotThrow()
        {
            // Arrange
            var user = new User("Teen", "User", "teen@example.com",
                new DateTime(2010, 1, 1)); // ровно 14 лет на 2024-01-01

            // Act & Assert
            var exception = Record.Exception(() => UserService.RegisterUser(user));
            Assert.Null(exception);
        }

        [Fact]
        public void RegisterUser_UserWithInvalidData_WrapsException()
        {
            // Arrange - создаем пользователя с данными, которые могут вызвать исключение
            var user = new User(null, null, null, DateTime.MinValue);

            // Act & Assert
            var exception = Assert.Throws<UserRegistrationException>(
                () => UserService.RegisterUser(user));

            Assert.NotNull(exception.InnerException);
        }

        [Fact]
        public void User_GetAge_CalculatesCorrectly()
        {
            // Arrange
            var user = new User("Test", "User", "test@example.com",
                new DateTime(2000, 6, 15)); // Родился 15 июня 2000

            // Act
            var ageBeforeBirthday = user.GetAge(new DateTime(2024, 6, 1)); // До дня рождения
            var ageAfterBirthday = user.GetAge(new DateTime(2024, 6, 20)); // После дня рождения
            var ageOnBirthday = user.GetAge(new DateTime(2024, 6, 15)); // В день рождения

            // Assert
            Assert.Equal(23, ageBeforeBirthday); // Еще не было 24 лет
            Assert.Equal(24, ageAfterBirthday); // Уже было 24 года
            Assert.Equal(24, ageOnBirthday); // В день рождения считается полный возраст
        }

        [Fact]
        public void GenerateUsers_ZeroCount_ReturnsEmptyList()
        {
            // Act
            var users = UserService.GenerateUsers(0);

            // Assert
            Assert.Empty(users);
        }

        [Fact]
        public void GenerateUsers_NegativeCount_ReturnsEmptyList()
        {
            // Act
            var users = UserService.GenerateUsers(-5);

            // Assert
            Assert.Empty(users);
        }
    }
}