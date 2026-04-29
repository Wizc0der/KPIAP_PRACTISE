class PersonFileReader
{
    private readonly string filePath;

    public PersonFileReader(string filePath) => this.filePath = filePath;

    public List<Person> ReadPeople()
    {
        var people = new List<Person>();
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Файл не найден: {filePath}");
            return people;
        }

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split(',');
            if (parts.Length == 2 && int.TryParse(parts[1], out int age))
                people.Add(new Person(parts[0], age));
        }
        return people;
    }
}
