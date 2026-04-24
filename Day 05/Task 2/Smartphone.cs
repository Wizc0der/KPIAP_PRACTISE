using static System.Console;
using System.Linq;

class Smartphone
{
    public string Model { get; set; }
    public Battery Battery { get; }
    public User Owner { get; set; }

    private App[] _apps = Array.Empty<App>();

    public App[] Apps { get { return _apps; } }

    public Smartphone(string model, int capacity, User owner)
    {
        Model = model;
        Battery = new Battery(capacity);
        Owner = owner;
    }

    public void InstallApp(App app)
    {
        Array.Resize(ref _apps, _apps.Length + 1);
        _apps[_apps.Length - 1] = app;
    }

    public void ShowInstalledApps()
    {
        WriteLine($"\n {Model} (Владелец: {Owner.Name}, Батарея: {Battery})");
        WriteLine("Приложения: " + (_apps.Any() ? string.Join(", ", _apps) : "нет"));
    }
}