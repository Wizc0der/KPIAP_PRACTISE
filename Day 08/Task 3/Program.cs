class Program
{
    static void Main()
    {
        // Логирование строк
        var stringLogger = new LoggerManager<string>(new ConsoleLogger<string>());
        stringLogger.LogWarning("Низкий уровень памяти");
        stringLogger.LogError("Не удалось подключиться к БД");
        stringLogger.Log("Пользователь вошел в систему");

        // Логирование чисел
        var intLogger = new LoggerManager<int>(new ConsoleLogger<int>());
        intLogger.LogError(404);
        intLogger.LogWarning(500);

        Console.WriteLine($"\nВсего логов (string): {stringLogger.Count}");
        Console.WriteLine($"Всего логов (int): {intLogger.Count}");
    }
}