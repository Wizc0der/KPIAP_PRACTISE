using static System.Console;

Write("Введите число: ");
string number = ReadLine();

string result = "";

foreach (char digit in number)
{

    if (digit == '-')
        continue;

    int num = digit - '0';
    if (num % 2 != 0)
    {
        result += digit;
    }
}

if (result == "")
    result = "0";

WriteLine($"Результат: {result}");