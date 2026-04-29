class Car
{
    public int Speed { get; private set; }

    public void SetSpeed(int speed)
    {
        if (speed > 120)
            throw new SpeedLimitExceededException(speed);
        Speed = speed;
        Console.WriteLine($"Скорость установлена: {speed} км/ч");
    }
}