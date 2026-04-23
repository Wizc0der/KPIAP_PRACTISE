using static System.Console;

// 23 ряда, 40 мест
int[,] hall = new int[23, 40];
Random rnd = new Random();

// Заполняем случайными 0 и 1 для демонстрации
for (int i = 0; i < 23; i++)
    for (int j = 0; j < 40; j++)
        hall[i, j] = rnd.Next(2); // 0 - свободно, 1 - продано

// Проверяем только первый ряд (индекс 0)
bool hasFree = false;
for (int j = 0; j < 40; j++)
{
    if (hall[0, j] == 0)
    {
        hasFree = true;
        break; // Нашли хотя бы одно свободное место, дальше искать не нужно
    }
}

WriteLine(hasFree
    ? "В первом ряду есть свободные места."
    : "В первом ряду нет свободных мест.");