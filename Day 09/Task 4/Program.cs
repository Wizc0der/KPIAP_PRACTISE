class Program
{
    static void Main()
    {
        string watchPath = "watch_folder";

        if (!Directory.Exists(watchPath))
            Directory.CreateDirectory(watchPath);

        var watcher = new FileWatcher(watchPath);
        watcher.Start();

        System.Threading.Thread.Sleep(1000);

        Console.WriteLine("=== Тестирование событий ===\n");

        File.WriteAllText(Path.Combine(watchPath, "test.txt"), "Привет");
        System.Threading.Thread.Sleep(500);

        File.AppendAllText(Path.Combine(watchPath, "test.txt"), " Мир");
        System.Threading.Thread.Sleep(500);

        File.Move(Path.Combine(watchPath, "test.txt"), Path.Combine(watchPath, "renamed.txt"));
        System.Threading.Thread.Sleep(500);

        File.Delete(Path.Combine(watchPath, "renamed.txt"));
        System.Threading.Thread.Sleep(500);

        Console.WriteLine("\n=== Тест завершён ===");
        watcher.Stop();

        if (Directory.Exists(watchPath))
            Directory.Delete(watchPath, true);
    }
}