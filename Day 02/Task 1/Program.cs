using static System.Console;


Write("Введите элементы массива через пробел: ");
string input = ReadLine();

// Разбиваем строку и игнорируем лишние пробелы
string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

if (parts.Length == 0)
{
    WriteLine("Массив пуст. Завершение программы.");
    return;
}

// Преобразуем в целочисленный массив
int[] arr = new int[parts.Length];
for (int i = 0; i < parts.Length; i++)
{
    arr[i] = int.Parse(parts[i]);
}

// --- Поиск минимального элемента и его индекса ---
int minIndex = 0;          // Начальный индекс первого элемента
int minValue = arr[0];     // Начальное значение минимума

for (int i = 1; i < arr.Length; i++)
{
    if (arr[i] < minValue)
    {
        minValue = arr[i];
        minIndex = i;
    }
}
WriteLine($"\nМинимальный элемент: {minValue}");
WriteLine($"Индекс в массиве (с 0): {minIndex}");
WriteLine($"Порядковый номер (с 1): {minIndex + 1}");