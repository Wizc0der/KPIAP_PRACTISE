using static System.Console;

Write("Введите x: ");
double x = double.Parse(ReadLine());

if (x > 1)
{
    WriteLine($"Результат: {Math.Log(2 * x) + Math.Sqrt(1 + Math.Pow(x, 2)):F4}");
} else
{
    WriteLine($"Результат: {2 * Math.Cos(x) + 3 * Math.Pow(x, 2):F4}");
}
