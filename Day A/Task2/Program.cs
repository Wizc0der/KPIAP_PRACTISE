using static System.Console;

Write("Введите двузначное число: ");

if (int.TryParse(ReadLine(), out int number))
{
    if (number >= 10 && number <=99)
    {
        int tens = number / 10;
        int units = number % 10;

        int swapped = units * 10 + tens;

        WriteLine($"Перевёрнутое число: {swapped}");

    } else
    {
        WriteLine("Ошибка: введите число от 10 до 99.");
    }
} else
{
    WriteLine("Ошибка: введите корректное целое число.");
}