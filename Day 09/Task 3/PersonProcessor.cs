class PersonProcessor
{
    public Dictionary<string, List<Person>> GroupByAge(List<Person> people)
    {
        return people.GroupBy(p =>
        {
            if (p.Age < 18) return "Младше 18";
            if (p.Age <= 40) return "18-40 лет";
            return "Старше 40";
        })
        .ToDictionary(g => g.Key, g => g.ToList());
    }

    public void PrintGroups(Dictionary<string, List<Person>> groups)
    {
        foreach (var group in groups.OrderBy(g => g.Key))
        {
            Console.WriteLine($"\n{group.Key}:");
            foreach (var person in group.Value)
                Console.WriteLine($"  {person}");
        }
    }
}