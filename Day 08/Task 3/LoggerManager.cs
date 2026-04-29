class LoggerManager<T>
{
    private List<T> logs = new List<T>();
    private ILogger<T> logger;

    public LoggerManager(ILogger<T> logger) => this.logger = logger;

    public void Log(T message)
    {
        logs.Add(message);
        logger.Log(message);
    }
    public void LogError(T message)
    {
        logs.Add(message);
        Console.Write($"ОШИБКА: ");
        logger.Log(message);
    }

    public void LogWarning(T message)
    {
        logs.Add(message);
        Console.Write($"ПРЕДУПРЕЖДЕНИЕ: ");
        logger.Log(message);
    }

    public void ShowAll()
    {
        Console.WriteLine("\nВсе логи:");
        foreach (var log in logs)
            Console.WriteLine($"   {log}");
    }

    public int Count => logs.Count;
}
