using System;

namespace App.Tasks;

public static class BasicValidation
{
    public static int ParseStringToInt(string input)
    {
        try
        {
            int result = int.Parse(input);
            return result;
        }
        catch (FormatException)
        {
            Console.WriteLine("Input string is not in a valid integer format.");
            return 0;
        }
        catch (OverflowException)
        {
            Console.WriteLine("The number is too large or too small for an Int32.");
            return 0;
        }
    }
}