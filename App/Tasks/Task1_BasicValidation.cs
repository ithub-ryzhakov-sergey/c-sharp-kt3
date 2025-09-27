using System;

namespace App.Tasks
{
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
                Console.WriteLine("Ошибка: неверный формат строки. Введите целое число.");
                return 0;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Ошибка: число слишком большое или слишком маленькое для типа int.");
                return 0;
            }
        }
    }
}
