using static System.Console;

Write("R1 (Ом) -> ");
double R1 = double.Parse(ReadLine().Replace('.', ','));

Write("R2 (Ом) -> ");
double R2 = double.Parse(ReadLine().Replace('.', ','));

Write("R3 (Ом) -> ");
double R3 = double.Parse(ReadLine().Replace('.', ','));

double R_total = 1.0 / (1.0 / R1 + 1.0 / R2 + 1.0 / R3);

WriteLine($"\nОбщее сопротивление: {R_total:F2} Ом");