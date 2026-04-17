using static System.Console;

Write("Введите цену 1 кг конфет: ");
double pricePerKg = double.Parse(ReadLine());

for (int i = 1; i <= 10; i++)
{
    double weight = i * 0.1;
    double cost = pricePerKg * weight;
    WriteLine($"{cost:F4}");
}