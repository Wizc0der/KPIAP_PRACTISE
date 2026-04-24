using System;

abstract class Transport
{
    public abstract void Move();
}

class Car : Transport
{
    public override void Move() => Console.WriteLine("🚗 Машина едет по дороге");
}

class Bike : Transport
{
    public override void Move() => Console.WriteLine("🚲 Велосипед крутит педали");
}

class Airplane : Transport
{
    public override void Move() => Console.WriteLine("✈️ Самолёт набирает высоту");
}

class Program
{
    static void Main()
    {
        Transport[] fleet = { new Car(), new Bike(), new Airplane() };
        foreach (var t in fleet) t.Move();
    }
}