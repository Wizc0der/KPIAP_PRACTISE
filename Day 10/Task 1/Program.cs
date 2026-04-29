class Program
{
    static void Main()
    {
        var settings1 = UISettings.GetInstance();
        var settings2 = UISettings.GetInstance();

        Console.WriteLine($"Один объект: {ReferenceEquals(settings1, settings2)}\n");

        Console.WriteLine($"Тема по умолчанию: {settings1.GetTheme()}");

        settings1.SetTheme("Тёмная");
        Console.WriteLine($"Тема через settings1: {settings1.GetTheme()}");

        settings2.SetTheme("Светлая");
        Console.WriteLine($"Тема через settings2: {settings2.GetTheme()}");

        Console.WriteLine($"\nТема в settings1: {settings1.GetTheme()}");
        Console.WriteLine($"Тема в settings2: {settings2.GetTheme()}");
    }
}