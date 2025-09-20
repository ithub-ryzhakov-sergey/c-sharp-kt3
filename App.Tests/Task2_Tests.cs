//using App.Tasks;
//using Xunit;

//namespace App.Tests;

//public class Task2_Tests
//{
//    [Fact]
//    public void ValidatePassword_Valid_NoException()
//    {
//        PasswordValidator.ValidatePassword("Valid123");
//    }

//    [Fact]
//    public void ValidatePassword_Null_Throws()
//    {
//        Assert.Throws<InvalidPasswordException>(() => PasswordValidator.ValidatePassword(null!));
//    }

//    [Fact]
//    public void ValidatePassword_TooShort_Throws()
//    {
//        Assert.Throws<InvalidPasswordException>(() => PasswordValidator.ValidatePassword("Abc12"));
//    }

//    [Fact]
//    public void ValidatePassword_NoDigit_Throws()
//    {
//        Assert.Throws<InvalidPasswordException>(() => PasswordValidator.ValidatePassword("Password"));
//    }
//}

