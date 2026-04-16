using static System.Console;


Write("Введите число: ");
int number = int.Parse(ReadLine());

int hungres = number / 100;
int tens = number % 100 / 10;
int units = number % 100 % 10;

WriteLine($"Результат: {units}{hungres}{tens}");
