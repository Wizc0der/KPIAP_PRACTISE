using System.Collections.Generic;

interface ILogger<T>
{
    void Log(T message);
}

class ConsoleLogger<T> : ILogger<T>
{
    public void Log(T message)
    {
        Console.WriteLine($"[{typeof(T).Name}] {message}");
    }
}