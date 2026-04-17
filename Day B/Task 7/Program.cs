using static System.Console;

Write("Введите стоимость 1 штуки товара (x): ");
double x = double.Parse(ReadLine());

WriteLine("Через for:");
WriteLine("Количество | Стоимость");
WriteLine("-----------+-----------");
for (int k = 10; k <= 200; k += 10)
{
    double cost = k * x;
    WriteLine($"{k,10} | {cost,10:F2}");
}

WriteLine("Через While:");
WriteLine("Количество | Стоимость");
WriteLine("-----------+-----------");
int count = 10;
while (count <= 200)
{
    double cost = count * x;
    WriteLine($"{count,10} | {cost,10:F2}");
    count += 10;
}

WriteLine("Через do while: ");
WriteLine("Количество | Стоимость");
WriteLine("-----------+-----------");
count = 10;
do
{
    double cost = count * x;
    WriteLine($"{count,10} | {cost,10:F2}");
    count += 10;
} while (count <= 200);

