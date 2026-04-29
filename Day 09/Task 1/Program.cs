class Program
{
    static void Main()
    {
        string folder = "test_files";
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

        string file = Path.Combine(folder, "nemets.as");
        string copy = Path.Combine(folder, "nemets_copy.as");
        string renamed = Path.Combine(folder, "nemets.io");
        string other = Path.Combine(folder, "other.as");

        // 1. Создать, записать, прочитать
        FileManager.CreateFile(file, "Привет, это файл nemets.as!");
        Console.WriteLine($"Содержимое: {File.ReadAllText(file)}\n");

        // 3. Информация о файле
        FileInfoProvider.ShowInfo(file);

        // 4. Копирование
        FileManager.CopyFile(file, copy);
        Console.WriteLine($"Копия существует: {File.Exists(copy)}\n");

        // 8. Сравнение размеров
        FileManager.CreateFile(other, "Коротко");
        FileManager.CompareFiles(file, other);

        // 5. Перемещение
        string moved = Path.Combine(folder, "moved", "nemets.as");
        Directory.CreateDirectory(Path.GetDirectoryName(moved));
        FileManager.MoveFile(copy, moved);

        // 6. Переименование
        FileManager.RenameFile(file, "nemets.io");

        // 2. Удаление с проверкой
        FileManager.DeleteFile(renamed);

        // 7. Ошибка при удалении несуществующего
        try { FileManager.DeleteFile(Path.Combine(folder, "no_such_file.txt")); }
        catch (Exception ex) { Console.WriteLine($"⚠Ошибка: {ex.Message}\n"); }

        // 9. Удаление по шаблону *.as
        FileManager.CreateFile(Path.Combine(folder, "temp.as"), "temp");
        FileManager.DeleteByPattern(folder, "*.as");

        // 10. Список файлов
        FileManager.ListFiles(folder);

        // 11-12. Права доступа
        FileManager.CreateFile(file, "Тест прав");
        FileManager.SetReadOnly(file, true);
        FileManager.CheckPermissions(file);
        try { File.WriteAllText(file, "Не получится"); }
        catch (UnauthorizedAccessException) { Console.WriteLine("Запись запрещена (ожидаемо)\n"); }
        FileManager.SetReadOnly(file, false);

        // Cleanup
        Console.WriteLine("\nОчистка...");
        foreach (var f in Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories)) File.Delete(f);
        foreach (var d in Directory.GetDirectories(folder)) Directory.Delete(d, true);
        Directory.Delete(folder);
        Console.WriteLine("Готово!");
    }
}