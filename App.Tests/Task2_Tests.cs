using Xunit;
using System;
using App.Tasks;

namespace App.Tests
{
    public class Task2_Tests
    {
        [Fact]
        public void InvalidPasswordException_Constructors_WorkCorrectly()
        {
            // Arrange & Act
            var ex1 = new InvalidPasswordException();
            var ex2 = new InvalidPasswordException("Custom message");
            var innerEx = new Exception("Inner exception");
            var ex3 = new InvalidPasswordException("Custom message", innerEx);

            // Assert
            Assert.NotNull(ex1);
            Assert.NotNull(ex2);
            Assert.NotNull(ex3);
            Assert.Equal("Custom message", ex2.Message);
            Assert.Equal("Custom message", ex3.Message);
            Assert.Same(innerEx, ex3.InnerException);
        }

        [Theory]
        [InlineData("ValidPass123")]
        [InlineData("LongPasswordWith123")]
        [InlineData("12345678")]
        [InlineData("P@ssw0rd")]
        public void ValidatePassword_ValidPassword_DoesNotThrow(string password)
        {
            // Act & Assert
            var exception = Record.Exception(() => Task2_CustomExceptions.ValidatePassword(password));
            Assert.Null(exception);
        }

        [Fact]
        public void ValidatePassword_NullPassword_ThrowsInvalidPasswordException()
        {
            // Arrange
            string password = null;

            // Act & Assert
            var exception = Assert.Throws<InvalidPasswordException>(
                () => Task2_CustomExceptions.ValidatePassword(password));

            Assert.Contains("не может быть null", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("short")]
        [InlineData("1234567")]
        public void ValidatePassword_TooShortPassword_ThrowsInvalidPasswordException(string password)
        {
            // Act & Assert
            var exception = Assert.Throws<InvalidPasswordException>(
                () => Task2_CustomExceptions.ValidatePassword(password));

            Assert.Contains("8 символов", exception.Message);
        }

        [Theory]
        [InlineData("password")]
        [InlineData("abcdefgh")]
        [InlineData("P@ssword")]
        [InlineData("OnlyLetters")]
        public void ValidatePassword_NoDigits_ThrowsInvalidPasswordException(string password)
        {
            // Act & Assert
            var exception = Assert.Throws<InvalidPasswordException>(
                () => Task2_CustomExceptions.ValidatePassword(password));

            Assert.Contains("хотя бы одну цифру", exception.Message);
        }

        [Theory]
        [InlineData("short")]
        [InlineData("nodigits")]
        public void ValidatePassword_MultipleViolations_ThrowsFirstViolationException(string password)
        {
            // Act & Assert
            var exception = Assert.Throws<InvalidPasswordException>(
                () => Task2_CustomExceptions.ValidatePassword(password));

            // Должна быть первая обнаруженная ошибка (длина)
            Assert.Contains("8 символов", exception.Message);
        }
    }
}