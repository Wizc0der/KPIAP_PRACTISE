class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Person(string name, int age) => (Name, Age) = (name, age);
    public string ToFileString() => $"{Name},{Age}";
    public override string ToString() => $"{Name} ({Age} лет)";
}
