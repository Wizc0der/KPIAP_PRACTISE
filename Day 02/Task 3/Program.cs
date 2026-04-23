using static System.Console;

Write("N (<10): ");
int N = int.Parse(ReadLine());
if (N <= 0 || N >= 10) { WriteLine("N должно быть от 1 до 9."); return; }

Write("a: "); int a = int.Parse(ReadLine());
Write("b: "); int b = int.Parse(ReadLine());
if (a > b) { int t = a; a = b; b = t; }

Write("D: "); int D = int.Parse(ReadLine());

int[,] m = new int[N, N];
Random rnd = new Random();

for (int i = 0; i < N; i++)
    for (int j = 0; j < N; j++)
        m[i, j] = rnd.Next(a, b + 1);

WriteLine("\nИсходная матрица:");
for (int i = 0; i < N; i++)
{
    for (int j = 0; j < N; j++)
        Write($"{m[i, j],5}");
    WriteLine();
}

int countLessD = 0;
foreach (int x in m)
    if (x < D) countLessD++;
WriteLine($"\nЧисел меньше {D}: {countLessD}");

WriteLine("Среднее по столбцам:");
for (int j = 0; j < N; j++)
{
    double sum = 0;
    for (int i = 0; i < N; i++) sum += m[i, j];
    WriteLine($"Столбец {j + 1}: {sum / N:F2}");
}