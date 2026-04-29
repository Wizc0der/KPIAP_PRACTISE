class Program
{
    static void Main()
    {
        var people = new List<Person>
        {
            new Person("Иван", 25),
            new Person("Мария", 16),
            new Person("Пётр", 30),
            new Person("Анна", 17),
            new Person("Дмитрий", 45)
        };

        var splitter = new PersonFileSplitter();
        splitter.WritePeople(people);

        splitter.ShowFile("adults.data");
        splitter.ShowFile("minors.data");

        File.Delete("adults.data");
        File.Delete("minors.data");
    }
}