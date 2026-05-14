using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.Data.Sqlite;

namespace HotelBooking.Services
{
    public class AuthService
    {
        private readonly DatabaseContext _db;
        public UserModel? CurrentUser { get; private set; }

        public AuthService(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<(bool Success, string Message)> LoginAsync(string username, string password)
        {
            string hash = AuthHelper.HashPassword(password);
            await using var conn = _db.GetConnection();
            await using var cmd = new SqliteCommand("""
                SELECT Id, Username, FullName, Role, Email, Phone
                FROM Users
                WHERE Username=@u AND PasswordHash=@h AND Role='Manager'
                """, conn);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@h", hash);

            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                CurrentUser = new UserModel
                {
                    Id       = reader.GetString(0),
                    Username = reader.GetString(1),
                    FullName = reader.GetString(2),
                    Role     = reader.GetString(3),
                    Email    = reader.GetString(4),
                    Phone    = reader.GetString(5)
                };
                return (true, $"Добро пожаловать, {CurrentUser.FullName}!");
            }
            return (false, "Неверный логин или пароль.");
        }

        public async Task<(bool Success, string Message)> RegisterManagerAsync(
            string username, string password, string fullName, string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(username)) return (false, "Введите логин.");
            if (password.Length < 6)                 return (false, "Пароль минимум 6 символов.");
            if (string.IsNullOrWhiteSpace(fullName)) return (false, "Введите ФИО.");

            await using var conn = _db.GetConnection();

            await using var check = new SqliteCommand(
                "SELECT COUNT(*) FROM Users WHERE Username=@u", conn);
            check.Parameters.AddWithValue("@u", username);
            var count = (long)(await check.ExecuteScalarAsync() ?? 0L);
            if (count > 0) return (false, "Такой логин уже занят.");

            await using var cmd = new SqliteCommand("""
                INSERT INTO Users (Id, Username, PasswordHash, FullName, Role, Email, Phone)
                VALUES (@id, @u, @h, @fn, 'Manager', @em, @ph)
                """, conn);
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("@u",  username);
            cmd.Parameters.AddWithValue("@h",  AuthHelper.HashPassword(password));
            cmd.Parameters.AddWithValue("@fn", fullName);
            cmd.Parameters.AddWithValue("@em", email);
            cmd.Parameters.AddWithValue("@ph", phone);
            await cmd.ExecuteNonQueryAsync();

            return (true, "Менеджер успешно зарегистрирован.");
        }

        public async Task RegisterGuestAsync(string fullName, string phone, string email)
        {
            await using var conn = _db.GetConnection();

            await using var find = new SqliteCommand(
                "SELECT COUNT(*) FROM Guests WHERE FullName=@fn AND Phone=@ph", conn);
            find.Parameters.AddWithValue("@fn", fullName);
            find.Parameters.AddWithValue("@ph", phone);
            var exists = (long)(await find.ExecuteScalarAsync() ?? 0L);
            if (exists > 0) return;

            await using var ins = new SqliteCommand(
                "INSERT INTO Guests (Id, FullName, Phone, Email) VALUES (@id, @fn, @ph, @em)", conn);
            ins.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
            ins.Parameters.AddWithValue("@fn", fullName);
            ins.Parameters.AddWithValue("@ph", phone);
            ins.Parameters.AddWithValue("@em", email);
            await ins.ExecuteNonQueryAsync();
        }

        public void Logout() => CurrentUser = null;
    }
}
