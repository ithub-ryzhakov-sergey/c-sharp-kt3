namespace App.Tasks;

public static class BasicValidation
{
    public static int ParseStringToInt(string input)
    {
        try
        {
            return int.Parse(input);
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: Введена строка в неверном формате.");
            return 0;
        }
        catch (OverflowException)
        {
            Console.WriteLine("Ошибка: Число выходит за пределы допустимого диапазона int.");
            return 0;
        }
    }
}

