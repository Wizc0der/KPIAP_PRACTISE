using static System.Console;

interface ISqlDatabase
{
    void Connect();
}

interface INoSqlDatabase
{
    void Connect();
}

class DatabaseConnector : ISqlDatabase, INoSqlDatabase
{
    void ISqlDatabase.Connect()
    {
        WriteLine("Подключение к SQL базе данных (MySQL/PostgreSQL)");
        WriteLine("   Установлено соединение через TCP/IP");
    }

    void INoSqlDatabase.Connect()
    {
        WriteLine("Подключение к NoSQL базе данных (MongoDB/Redis)");
        WriteLine("   Установлено соединение через драйвер");
    }
}

class Program
{
    static void Main()
    {
        DatabaseConnector connector = new DatabaseConnector();
        WriteLine("=== ЧЕРЕЗ ISqlDatabase ===");
        ISqlDatabase sql = connector;
        sql.Connect();

        WriteLine("\n=== ЧЕРЕЗ INoSqlDatabase ===");
        INoSqlDatabase nosql = connector;
        nosql.Connect();

        WriteLine("\n=== ПРЯМОЙ ВЫЗОВ ===");
        ((ISqlDatabase)connector).Connect();
        ((INoSqlDatabase)connector).Connect();
    }
}