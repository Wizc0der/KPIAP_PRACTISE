class Program
{
    static void Main()
    {
        var hub = new SmartHomeHub();


        hub.AddDevice(new Light());
        hub.AddDevice(new Thermostat());
        hub.AddDevice(new Alarm());

        hub.SetState("День");
        hub.SetState("Ночь");
        hub.SetState("Тревога");
    }
}