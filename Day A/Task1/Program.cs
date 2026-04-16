using static System.Console;

Write("Цена тетради (руб.) -> ");
decimal notebookPrice = decimal.Parse(ReadLine().Replace('.', ','));
Write("Цена обложки (руб.) -> ");
decimal coverPrice = decimal.Parse(ReadLine().Replace('.', ','));
Write("Количество комплектов (шт.) -> ");
int quantity = int.Parse(ReadLine());

decimal totoalCost = (notebookPrice + coverPrice) * quantity;

WriteLine($"Стоимость покупки: {totoalCost:F2} руб.");