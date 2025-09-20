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
                Console.WriteLine("������: ������������ ������ �����.");
                return 0;
            }
            catch (OverflowException)
            {
                Console.WriteLine("������: ����� ������� ������� ��� ������� ���������.");
                return 0;
            }
        }
    }
}