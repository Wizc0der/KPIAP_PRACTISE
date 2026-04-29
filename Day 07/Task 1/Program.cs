class Program
{
    static void Main()
    {
        var car = new Car();

        try
        {
            car.SetSpeed(100);
            car.SetSpeed(150);
        }
        catch (SpeedLimitExceededException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}