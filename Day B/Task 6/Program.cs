using static System.Console;
Write("Введите пол (м/ж): ");
string input = ReadLine();

char gender = char.ToLower(input.Trim()[0]);
switch (gender)
{
    case 'м':
        WriteLine("Возможные мужские имена: Александр, Дмитрий, Максим, Артём, Иван, Сергей, Павел, Станиславович, Николай.");
        break;
    case 'ж':
        WriteLine("Возможные женские имена: Анна, Мария, Елена, Ольга, Татьяна, Наталья, Виктория, Екатерина, Ирина.");
        break;
    default:
        WriteLine("Ошибка: введите только 'м' или 'ж'.");
        break;
}