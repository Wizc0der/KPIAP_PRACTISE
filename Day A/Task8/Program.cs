using static System.Console;

double x = 0.5;

double y = x * Math.Exp(x * x) -
           (Math.Sin(Math.Sqrt(x)) + Math.Pow(Math.Cos(Math.Log(x)), 2)) /
           Math.Sqrt(Math.Abs(1 - Math.PI * x));

WriteLine($"При x = {x}");
WriteLine($"y = {y}");