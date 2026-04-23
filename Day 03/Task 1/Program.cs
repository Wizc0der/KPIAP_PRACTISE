using static System.Console;

class A
{
    public int a;
    public int b;
    public A(int a, int b)
    {
        this.a = a;
        this.b = b;
    }

    public double GetArithmeticMean()
    {
        return (a + b) / 2.0;
    }
    public double CalculateExpression()
    {
        return Math.Pow(b, 3) + Math.Sqrt(a);
    }
}

class Program
{
    static void Main()
    {
        A obj = new A(9, 4);

        WriteLine($"a = {obj.a}");
        WriteLine($"b = {obj.b}");

        // Демонстрация работы с методами
        WriteLine("\n=== Работа с методами ===");
        WriteLine($"Среднее арифметическое a и b: {obj.GetArithmeticMean()}");
        WriteLine($"b^2 + √a = {obj.CalculateExpression()}");

        // Пример с другими значениями
        WriteLine("\n=== Пример с другими значениями ===");
        A obj2 = new A(16, 5);
        WriteLine($"a = {obj2.a}, b = {obj2.b}");
        WriteLine($"Среднее арифметическое: {obj2.GetArithmeticMean()}");
        WriteLine($"b^2 + √a = {obj2.CalculateExpression()}");
    }
}