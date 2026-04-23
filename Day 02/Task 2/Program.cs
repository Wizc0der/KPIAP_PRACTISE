using static System.Console;

Write("n = ");
int n = int.Parse(ReadLine());

int[] a = new int[n];
WriteLine("Введите массив через пробел:");
string[] parts = ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
for (int i = 0; i < n; i++) a[i] = int.Parse(parts[i]);

int sumPos = 0, cntNeg = 0, cntZero = 0;
foreach (int x in a)
{
    if (x > 0) sumPos += x;
    else if (x < 0) cntNeg++;
    else cntZero++;
}

WriteLine($"\nСумма положительных: {sumPos}");
WriteLine($"Отрицательных: {cntNeg}, Нулевых: {cntZero}");

Array.Sort(a);
WriteLine("Отсортированный: " + string.Join(" ", a));

Write("Введите k для поиска: ");
int k = int.Parse(ReadLine());

int idx = Array.BinarySearch(a, k);
WriteLine(idx >= 0
    ? $"Найдено! Индекс: {idx}, Порядковый номер: {idx + 1}"
    : $"Число {k} не найдено.");