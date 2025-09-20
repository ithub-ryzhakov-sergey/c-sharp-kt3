namespace App.Tasks;

public static class BasicValidation
{
    public static int ParseStringToInt(string input)
    {
        try
        {
            int res = int.Parse(input);
            return res;
        }
        catch (FormatException)
        {
            Console.WriteLine("ошибка: неверный формат ввода");
            return 0;
        }
        catch (OverflowException)
        {
            Console.WriteLine("ошибка: тип данных int переполнен");
            return 0;
        }
    }
}
