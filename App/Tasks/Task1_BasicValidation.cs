namespace App.Tasks;

public static class BasicValidation
{
    public static int ParseStringToInt(string input)
    {
        try
        {
            Convert.ToInt32(input);
            return int.Parse(input);
        }

        catch (FormatException ex)
        {
            Console.WriteLine("Ошибка");
            return 0;
        }

        catch (OverflowException ex)
        {
            Console.WriteLine("Ошибка");
            return 0;
        }
    }
}

