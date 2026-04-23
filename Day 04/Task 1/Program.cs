using static System.Console;

static string ToBinary(int n) => Convert.ToString(n, 2);

Write("Введите число: ");
int x = int.Parse(ReadLine());
WriteLine($"Двоичное: {ToBinary(x)}");