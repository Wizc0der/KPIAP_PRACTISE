interface IDevice
{
    void Update(string state);
}

class Light : IDevice
{
    public void Update(string state)
    {
        if (state == "Ночь")
            Console.WriteLine("Свет: Выключен");
        else if (state == "День")
            Console.WriteLine("Свет: Включен");
        else if (state == "Тревога")
            Console.WriteLine("Свет: Мигает");
    }
}

class Thermostat : IDevice
{
    public void Update(string state)
    {
        if (state == "Ночь")
            Console.WriteLine("Термостат: 18°C (экономия)");
        else if (state == "День")
            Console.WriteLine("Термостат: 22°C (комфорт)");
        else if (state == "Тревога")
            Console.WriteLine("Термостат: Выключен");
    }
}

class Alarm : IDevice
{
    public void Update(string state)
    {
        if (state == "Тревога")
            Console.WriteLine("Сигнализация: ВКЛЮЧЕНА!");
        else
            Console.WriteLine("Сигнализация: Выключена");
    }
}

class SmartHomeHub
{
    private List<IDevice> devices = new List<IDevice>();
    private string currentState;

    public void AddDevice(IDevice device)
    {
        devices.Add(device);
    }

    public void SetState(string state)
    {
        currentState = state;
        Console.WriteLine($"\nХаб: Режим '{state}'");
        Notify();
    }

    private void Notify()
    {
        foreach (var device in devices)
        {
            device.Update(currentState);
        }
    }
}