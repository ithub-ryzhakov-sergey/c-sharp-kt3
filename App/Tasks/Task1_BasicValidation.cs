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
            System.Console.WriteLine("������ ������� ������.");
            return 0;
        }
        catch (OverflowException)
        {
            System.Console.WriteLine("������������ ��������� ��������.");
            return 0;
        }
    }
}