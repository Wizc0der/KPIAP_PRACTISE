class FileWatcher
{
    public event EventHandler<string> FileChanged;

    public void ChangeFile(string fileName)
    {
        Console.WriteLine($"Файл изменён: {fileName}");
        FileChanged?.Invoke(this, fileName);
    }
}
