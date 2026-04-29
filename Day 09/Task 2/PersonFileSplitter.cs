class PersonFileSplitter
{
    const string AdultsFile = "adults.data";
    const string MinorsFile = "minors.data";

    public void WritePeople(List<Person> people)
    {
        File.Delete(AdultsFile); File.Delete(MinorsFile);

        foreach (var p in people)
        {
            string file = p.Age >= 18 ? AdultsFile : MinorsFile;
            File.AppendAllText(file, p + "\n");
        }
        Console.WriteLine($"Записано: {people.Count} человек");
    }

    public void ShowFile(string path)
    {
        Console.WriteLine($"\n{path}:");
        if (File.Exists(path))
            foreach (var line in File.ReadAllLines(path))
                Console.WriteLine($"   {line}");
        else
            Console.WriteLine("   (пусто)");
    }
}