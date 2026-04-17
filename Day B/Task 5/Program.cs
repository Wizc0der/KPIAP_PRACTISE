using static System.Console;

Write("Введите число: ");
int n = int.Parse(ReadLine());

WriteLine(Math.Abs(n % 10) == 7 ? "Да" : "Нет");