using static System.Console;

Write("Введите исходный текст: ");
string text = ReadLine();

Write("Какое слово заменить: ");
string oldWord = ReadLine();

Write("На что заменить: ");
string newWord = ReadLine();

string result = text.Replace(oldWord, newWord, StringComparison.OrdinalIgnoreCase);

WriteLine("\nРезультат:");
WriteLine(result);