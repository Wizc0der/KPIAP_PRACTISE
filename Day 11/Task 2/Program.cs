using System;


class Program
{
    static void Main()
    {
        ICar car1 = new BasicCar();
        Console.WriteLine($"{car1.GetFeatures()} - ${car1.GetPrice()}");

        ICar car2 = new SunroofDecorator(new BasicCar());
        Console.WriteLine($"{car2.GetFeatures()} - ${car2.GetPrice()}");

        ICar car3 = new NavigationDecorator(new SunroofDecorator(new BasicCar()));
        Console.WriteLine($"{car3.GetFeatures()} - ${car3.GetPrice()}");

        ICar car4 = new LeatherSeatsDecorator(new NavigationDecorator(new SunroofDecorator(new BasicCar())));
        Console.WriteLine($"{car4.GetFeatures()} - ${car4.GetPrice()}");
    }
}