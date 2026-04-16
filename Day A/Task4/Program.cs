using static System.Console;

Write("a= ");
decimal a = decimal.Parse(ReadLine().Replace('.', ','));
Write("b= ");
decimal b = decimal.Parse(ReadLine().Replace('.', ','));

decimal totalCost = a / b;

WriteLine($"{a}/{b}={totalCost:F3}");