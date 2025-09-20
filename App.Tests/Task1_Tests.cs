//using App.Tasks;
//using Xunit;

//namespace App.Tests;

//public class Task1_Tests
//{
//    [Fact]
//    public void ParseStringToInt_ValidNumber_ReturnsParsed()
//    {
//        var result = BasicValidation.ParseStringToInt("123");
//        Assert.Equal(123, result);
//    }

//    [Fact]
//    public void ParseStringToInt_InvalidString_ReturnsZero()
//    {
//        var result = BasicValidation.ParseStringToInt("abc");
//        Assert.Equal(0, result);
//    }

//    [Fact]
//    public void ParseStringToInt_Overflow_ReturnsZero()
//    {
//        // Превышает диапазон Int32
//        var result = BasicValidation.ParseStringToInt("9999999999999");
//        Assert.Equal(0, result);
//    }
//}

