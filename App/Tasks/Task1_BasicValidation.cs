namespace App.Tasks;

public static class BasicValidation
{
    public static int ParseStringToInt(string input)
    {
        try
        {
            return int.Parse(input);
        }

        catch(OverflowException) 
        {
            Console.WriteLine("Превышен лимит");
            return 0;
        }
        catch (FormatException)
        {
            Console.WriteLine("ошибка формата");
            return 0;
        }


    }
}

