using static System.Console;

abstract class Device
{
    public abstract void TurnOn();
    public virtual void TurnOff() => WriteLine("Device is turning off");
}

class Smartphone : Device
{
    public override void TurnOn() => WriteLine("Smartphone is turning on");
    public override void TurnOff() => WriteLine("Smartphone is turning off");
}

class Laptop : Device
{
    public override void TurnOn() => WriteLine("Laptop is turning on");
    public override void TurnOff() => WriteLine("Laptop is turning off");
}

class Program
{
    static void Main()
    {
        Device phone = new Smartphone();
        Device laptop = new Laptop();

        phone.TurnOn();
        phone.TurnOff();

        WriteLine();

        laptop.TurnOn();
        laptop.TurnOff();
    }
}