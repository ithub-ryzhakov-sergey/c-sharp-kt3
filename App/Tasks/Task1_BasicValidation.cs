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
            Console.WriteLine("");
            return 0;
        }
        catch (Exception)
        {
            Console.WriteLine("");
            return 0;
        }
    }
}

