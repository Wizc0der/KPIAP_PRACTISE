class Program
{
    static void Main()
    {
        var manager = new LinkedListManager<string>();

        manager.Add("Первый");
        manager.Add("Второй");
        manager.Add("Третий");

        Console.WriteLine("Список:");
        manager.ShowAll();
        Console.WriteLine($"Всего: {manager.Count}\n");

        manager.Find("Второй");
        manager.Find("Четвёртый");

        Console.WriteLine();
        manager.Remove("Второй");
        manager.Remove("Пятый");

        Console.WriteLine("\nПосле удаления:");
        manager.ShowAll();
    }
}