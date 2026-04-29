class FileWatcher
{
    private FileSystemWatcher watcher;
    private readonly string path;

    public FileWatcher(string path)
    {
        this.path = path;
        watcher = new FileSystemWatcher(path);
        watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;
        watcher.Filter = "*.*";

        watcher.Created += OnCreated;
        watcher.Deleted += OnDeleted;
        watcher.Changed += OnChanged;
        watcher.Renamed += OnRenamed;
    }

    public void Start()
    {
        watcher.EnableRaisingEvents = true;
        Console.WriteLine($"Мониторинг запущен: {path}");
        Console.WriteLine("Ожидание изменений... (нажмите Enter для выхода)\n");
    }

    public void Stop()
    {
        watcher.EnableRaisingEvents = false;
        watcher.Dispose();
        Console.WriteLine("Мониторинг остановлен");
    }

    // Обработка создания файла
    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"[СОЗДАН] {e.Name}");
        SendEmailNotification($"Создан новый файл: {e.Name}");
    }

    // Обработка удаления файла
    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"[УДАЛЁН] {e.Name}");
    }

    // Обработка изменения файла
    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"[ИЗМЕНЁН] {e.Name}");
    }

    // Обработка переименования файла
    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        Console.WriteLine($"[ПЕРЕИМЕНОВАН] {e.OldName} → {e.Name}");
    }

    // Имитация отправки email
    private void SendEmailNotification(string message)
    {
        Console.WriteLine($"     EMAIL: {message}");
        Console.WriteLine($"     Кому: admin@example.com");
        Console.WriteLine($"     Тема: Уведомление о файле");
        Console.WriteLine($"     Текст: {message}\n");
    }
}
