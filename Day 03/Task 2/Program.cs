using static System.Console;
using System.Linq;

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Height { get; set; }
    public override string ToString() => $"[{Name}, {Age}л, {Height}см]";
}

static class Gen
{
    static readonly Random R = new();
    static readonly string[] Names = { "Артём", "Иван", "Мария", "Анна", "Дмитрий" };
    public static Person[] GenerateRandomPersons(int n) => Enumerable.Range(0, n).Select(_ =>
        new Person { Name = Names[R.Next(Names.Length)], Age = R.Next(15, 61), Height = Math.Round(R.NextDouble() * 40 + 150, 1) }).ToArray();
}

static class Ops
{
    public static void SortByAge(Person[] a) => Array.Sort(a, (x, y) => x.Age.CompareTo(y.Age));
    public static Person[] FilterAge(Person[] a, int min) => a.Where(p => p.Age >= min).ToArray();
    public static double AvgAge(Person[] a) => a.Average(p => p.Age);
    public static Person Tallest(Person[] a) => a.OrderByDescending(p => p.Height).First();
}

class Program
{
    static void Main()
    {
        var arr = Gen.GenerateRandomPersons(5);
        WriteLine("Сгенерировано:\n" + string.Join("\n", arr));

        WriteLine($"\nСр. возраст: {Ops.AvgAge(arr):F1}");
        WriteLine($"Самый высокий: {Ops.Tallest(arr)}");

        Ops.SortByAge(arr);
        WriteLine("\nОтсортировано по возрасту:\n" + string.Join("\n", arr));

        WriteLine($"\nСтарше 25 лет:\n" + string.Join("\n", Ops.FilterAge(arr, 25)));
    }
}