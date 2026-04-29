class Program
{
    static void Main()
    {
        var manager = new ServiceRequestManager();

        manager.AddRequest("Немец", "Ремонт");
        manager.AddRequest("Массальский", "Консультация");
        manager.AddRequest("Федосик", "Настройка");

        manager.ShowAll();

        Console.WriteLine($"\nВсего заявок: {manager.Count}");

        Console.WriteLine("\nОбработка:");
        manager.ProcessNext();
        manager.ProcessNext();

        manager.ShowAll();
    }
}