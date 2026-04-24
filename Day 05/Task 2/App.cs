class App
{
    public string Name { get; set; }
    public App(string name) => Name = name;
    public override string ToString() => Name;
}