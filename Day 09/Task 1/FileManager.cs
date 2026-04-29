class FileManager
{
    public static void CreateFile(string path, string content)
    {
        File.WriteAllText(path, content);
        Console.WriteLine($"+ Создан: {path}");
    }

    public static void CopyFile(string source, string dest)
    {
        File.Copy(source, dest, true);
        Console.WriteLine($"+ Скопирован: {source} -> {dest}");
    }

    public static void MoveFile(string source, string dest)
    {
        File.Move(source, dest);
        Console.WriteLine($"+ Перемещён: {source} -> {dest}");
    }

    public static void RenameFile(string path, string newName)
    {
        File.Move(path, Path.Combine(Path.GetDirectoryName(path), newName));
        Console.WriteLine($"+ Переименован: {Path.GetFileName(path)} -> {newName}");
    }

    public static bool DeleteFile(string path)
    {
        if (File.Exists(path)) { File.Delete(path); Console.WriteLine($"+ Удалён: {path}"); return true; }
        Console.WriteLine($"- Не найден: {path}"); return false;
    }

    public static void DeleteByPattern(string folder, string pattern)
    {
        foreach (var f in Directory.GetFiles(folder, pattern)) { File.Delete(f); Console.WriteLine($"- Удалён: {Path.GetFileName(f)}"); }
    }

    public static void ListFiles(string folder)
    {
        Console.WriteLine("* Файлы:");
        foreach (var f in Directory.GetFiles(folder)) Console.WriteLine($"   {Path.GetFileName(f)}");
    }

    public static long CompareFiles(string f1, string f2)
    {
        var s1 = new FileInfo(f1).Length;
        var s2 = new FileInfo(f2).Length;
        Console.WriteLine($"{Path.GetFileName(f1)}: {s1} байт, {Path.GetFileName(f2)}: {s2} байт");
        return s1 - s2;
    }

    public static void SetReadOnly(string path, bool readOnly)
    {
        var f = new FileInfo(path); f.IsReadOnly = readOnly;
        Console.WriteLine($"Запись {(readOnly ? "запрещена" : "разрешена")}: {path}");
    }

    public static void CheckPermissions(string path)
    {
        var f = new FileInfo(path);
        Console.WriteLine($"{Path.GetFileName(path)}: Read={!(f.IsReadOnly)}, Write={!f.IsReadOnly}, Execute=N/A");
    }
}
