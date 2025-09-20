using Xunit;
using System;
using App.Tasks;

namespace App.Tests
{
    public class Task1_Tests
    {
        [Theory]
        [InlineData("123", 123)]
        [InlineData("-456", -456)]
        [InlineData("0", 0)]
        [InlineData("2147483647", int.MaxValue)]
        [InlineData("-2147483648", int.MinValue)]
        public void ParseStringToInt_ValidInput_ReturnsParsedInt(string input, int expected)
        {
            // Act
            var result = Task1_BasicValidation.ParseStringToInt(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("12.34")]
        [InlineData("123abc")]
        [InlineData("")]
        [InlineData(null)]
        public void ParseStringToInt_InvalidFormat_ReturnsZeroAndOutputsMessage(string input)
        {
            // Arrange
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var result = Task1_BasicValidation.ParseStringToInt(input);

            // Assert
            Assert.Equal(0, result);
            Assert.Contains("Некорректный формат", consoleOutput.ToString());
        }

        [Theory]
        [InlineData("99999999999999999999")]
        [InlineData("-99999999999999999999")]
        public void ParseStringToInt_Overflow_ReturnsZeroAndOutputsMessage(string input)
        {
            // Arrange
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var result = Task1_BasicValidation.ParseStringToInt(input);

            // Assert
            Assert.Equal(0, result);
            Assert.Contains("слишком большое", consoleOutput.ToString());
        }
    }
}