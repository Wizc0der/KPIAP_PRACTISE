using static System.Console;
using System.Linq;

abstract class ITSpecialist
{
    public string Name { get; set; }
    public ITSpecialist(string name) => Name = name;
}
interface IProgrammer
{
    void WriteCode();
    string Language { get; }
}

interface IDesigner
{
    void CreateDesign();
    string Tool { get; }
}

class BackendDeveloper : ITSpecialist, IProgrammer
{
    public string Language { get; }
    public BackendDeveloper(string name, string lang) : base(name) => Language = lang;
    public void WriteCode() => WriteLine($"{Name} пишет код на {Language}");
}

class UXDesigner : ITSpecialist, IDesigner
{
    public string Tool { get; }
    public UXDesigner(string name, string tool) : base(name) => Tool = tool;
    public void CreateDesign() => WriteLine($"{Name} создаёт дизайн в {Tool}");
}

class Program
{
    static void Main()
    {
        ITSpecialist[] team = {
            new BackendDeveloper("Артём", "C#"),
            new UXDesigner("Мария", "Figma"),
            new BackendDeveloper("Иван", "Python"),
            new UXDesigner("Анна", "Adobe XD"),
            new BackendDeveloper("Дмитрий", "Java")
        };

        WriteLine("=== ВСЯ КОМАНДА ===");
        foreach (var specialist in team)
            WriteLine(specialist.Name);
        WriteLine("\n=== ПРОГРАММИСТЫ ===");
        var programmers = team.OfType<IProgrammer>();
        foreach (var prog in programmers)
            prog.WriteCode();

        WriteLine("\n=== ДИЗАЙНЕРЫ ===");
        var designers = team.OfType<IDesigner>();
        foreach (var des in designers)
            des.CreateDesign();

        WriteLine($"\nВсего программистов: {programmers.Count()}");
        WriteLine($"Всего дизайнеров: {designers.Count()}");
    }
}