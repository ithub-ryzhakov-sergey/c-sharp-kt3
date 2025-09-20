using System;

namespace App.Tasks
{
    public static class Task1_BasicValidation
    {
        public static int ParseStringToInt(string input)
        {
            try
            {
                return int.Parse(input);
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Некорректный формат числа.");
                return 0;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Ошибка: Число слишком большое или слишком маленькое.");
                return 0;
            }
        }
    }
}