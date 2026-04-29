class FileInfoProvider
{
    public static void ShowInfo(string path)
    {
        if (!File.Exists(path)) { Console.WriteLine($"- Файл не найден: {path}"); return; }
        var f = new FileInfo(path);
        Console.WriteLine($"\n{f.Name}");
        Console.WriteLine($"   Размер: {f.Length} байт");
        Console.WriteLine($"   Создан: {f.CreationTime}");
        Console.WriteLine($"   Изменён: {f.LastWriteTime}");
    }
}