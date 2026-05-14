using System.IO;
using Microsoft.Data.Sqlite;

namespace HotelBooking.Data
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext()
        {
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(appDir, "hotel.db");
            _connectionString = $"Data Source={dbPath}";
            InitializeDatabase();
        }

        public SqliteConnection GetConnection()
        {
            var conn = new SqliteConnection(_connectionString);
            conn.Open();
            return conn;
        }

        private void InitializeDatabase()
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();

            // ── Create tables ──────────────────────────────────────────
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Rooms (
                    RoomNumber  INTEGER PRIMARY KEY,
                    Type        TEXT    NOT NULL,
                    PricePerNight REAL  NOT NULL,
                    Capacity    INTEGER NOT NULL,
                    Description TEXT    NOT NULL,
                    IsAvailable INTEGER NOT NULL DEFAULT 1
                );

                CREATE TABLE IF NOT EXISTS Bookings (
                    Id            TEXT PRIMARY KEY,
                    GuestFullName TEXT NOT NULL,
                    GuestPhone    TEXT NOT NULL,
                    GuestEmail    TEXT NOT NULL DEFAULT '',
                    RoomNumber    INTEGER NOT NULL,
                    CheckIn       TEXT NOT NULL,
                    CheckOut      TEXT NOT NULL,
                    TotalPrice    REAL NOT NULL,
                    Status        TEXT NOT NULL DEFAULT 'Активно',
                    FOREIGN KEY (RoomNumber) REFERENCES Rooms(RoomNumber)
                );

                CREATE TABLE IF NOT EXISTS Users (
                    Id           TEXT PRIMARY KEY,
                    Username     TEXT NOT NULL UNIQUE,
                    PasswordHash TEXT NOT NULL,
                    FullName     TEXT NOT NULL,
                    Role         TEXT NOT NULL DEFAULT 'Manager',
                    Email        TEXT NOT NULL DEFAULT '',
                    Phone        TEXT NOT NULL DEFAULT ''
                );

                CREATE TABLE IF NOT EXISTS Guests (
                    Id       TEXT PRIMARY KEY,
                    FullName TEXT NOT NULL,
                    Phone    TEXT NOT NULL,
                    Email    TEXT NOT NULL DEFAULT ''
                );
            ";
            cmd.ExecuteNonQuery();

            // ── Seed rooms if empty ────────────────────────────────────
            cmd.CommandText = "SELECT COUNT(*) FROM Rooms;";
            long roomCount = (long)(cmd.ExecuteScalar() ?? 0L);

            if (roomCount == 0)
            {
                cmd.CommandText = @"
                    INSERT INTO Rooms VALUES (101,'Стандарт',2500,1,'Уютный одноместный номер с видом на город',1);
                    INSERT INTO Rooms VALUES (102,'Стандарт',3200,2,'Двухместный номер с двуспальной кроватью',1);
                    INSERT INTO Rooms VALUES (103,'Стандарт',2800,2,'Стандартный номер с балконом',1);
                    INSERT INTO Rooms VALUES (104,'Эконом',1800,1,'Бюджетный вариант, всё необходимое есть',1);
                    INSERT INTO Rooms VALUES (201,'Люкс',5500,2,'Просторный люкс с джакузи и панорамным видом',1);
                    INSERT INTO Rooms VALUES (202,'Люкс',6000,3,'Семейный люкс с двумя комнатами',1);
                    INSERT INTO Rooms VALUES (203,'Полулюкс',4200,2,'Полулюкс с гостиной зоной',1);
                    INSERT INTO Rooms VALUES (301,'Президентский',12000,4,'Эксклюзивный президентский номер, весь этаж',1);
                ";
                cmd.ExecuteNonQuery();
            }

            // ── Seed default admin if no users ─────────────────────────
            cmd.CommandText = "SELECT COUNT(*) FROM Users;";
            long userCount = (long)(cmd.ExecuteScalar() ?? 0L);

            if (userCount == 0)
            {
                string hash = AuthHelper.HashPassword("admin123");
                cmd.CommandText = @"
                    INSERT INTO Users (Id, Username, PasswordHash, FullName, Role, Email, Phone)
                    VALUES (@id, 'admin', @hash, 'Администратор', 'Manager', 'admin@hotel.by', '+375 29 000-00-00');
                ";
                cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                cmd.Parameters.AddWithValue("@hash", hash);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
