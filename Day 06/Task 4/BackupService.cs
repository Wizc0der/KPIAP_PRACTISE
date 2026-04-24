class BackupService
{
    public void OnFileChanged(object sender, string file)
    {
        Console.WriteLine($"Бэкап: {file}");
    }
}
