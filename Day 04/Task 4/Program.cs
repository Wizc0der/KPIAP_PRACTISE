using static System.Console;

public static class DateTimeExtensions
{
    public static int GetAge(this DateTime birthDate)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age))
            age--;

        return age;
    }
}

class Program
{
    static void Main()
    {
        DateTime dob = new DateTime(2007, 12, 12);
        WriteLine($"Возраст: {dob.GetAge()} лет");
    }
}