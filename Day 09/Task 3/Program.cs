class Program
{
    static void Main()
    {
        string filePath = "file.data";

        var testPeople = new List<Person>
        {
            new Person("Иван", 25),
            new Person("Мария", 16),
            new Person("Пётр", 30),
            new Person("Анна", 17),
            new Person("Дмитрий", 45),
            new Person("Елена", 12),
            new Person("Сергей", 55)
        };

        File.WriteAllLines(filePath, testPeople.Select(p => p.ToFileString()));
        Console.WriteLine($"Файл создан: {filePath}\n");

        var reader = new PersonFileReader(filePath);
        var people = reader.ReadPeople();
        Console.WriteLine($"Прочитано человек: {people.Count}\n");

        var processor = new PersonProcessor();
        var groups = processor.GroupByAge(people);
        processor.PrintGroups(groups);

        File.Delete(filePath);
    }
}