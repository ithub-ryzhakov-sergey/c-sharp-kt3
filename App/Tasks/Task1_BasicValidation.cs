using System;

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
            // Сообщение об ошибке для неверного формата
            Console.WriteLine("Ошибка: неверный формат строки. Введите целое число.");
            return 0;
        }
        catch (OverflowException)
        {
            // Сообщение об ошибке для переполнения
            Console.WriteLine("Ошибка: число слишком большое или слишком маленькое для типа int.");
            return 0;
        }
    }
}