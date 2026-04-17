using static System.Console;

Write("Введите число: ");
int number = int.Parse(ReadLine());
if (number < 100 || number > 999)
{
    WriteLine("Число должно быть трёхзначным!");
    return;
}

if (number % 2 == 0)
{
    WriteLine("False - Число чётное");
} else
{
    WriteLine("True - Число не чётное");
}