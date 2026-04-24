class Logger
{
    public void OnFileChanged(object sender, string file)
    {
        Console.WriteLine($"Лог: {file}");
    }
}