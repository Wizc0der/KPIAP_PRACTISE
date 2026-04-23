using static System.Console;

Write("Введите строку: ");
string s = ReadLine() ?? "";

WriteLine($"Верхний регистр: {s.ToUpper()}");
WriteLine($"Нижний регистр: {s.ToLower()}");