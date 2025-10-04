namespace App.Tasks;

public static class BasicValidation
{
    public static int ParseStringToInt(string input)
    {
        try
        {
            return int.Parse(input);
        }
        catch (FormatException err)
        {
            System.Console.WriteLine(err);
        }
        catch (OverflowException err)
        {
            System.Console.WriteLine(err);
        }
        return 0;
    }
}

