class Program
{
    static void Main()
    {
        var watcher = new FileWatcher();
        var backup = new BackupService();
        var logger = new Logger();
        new FileMonitor(watcher, backup, logger);

        watcher.ChangeFile("doc.txt");
        watcher.ChangeFile("data.csv");
    }
}