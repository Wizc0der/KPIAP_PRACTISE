class FileMonitor
{
    public FileMonitor(FileWatcher watcher, BackupService backup, Logger logger)
    {
        watcher.FileChanged += backup.OnFileChanged;
        watcher.FileChanged += logger.OnFileChanged;
    }
}