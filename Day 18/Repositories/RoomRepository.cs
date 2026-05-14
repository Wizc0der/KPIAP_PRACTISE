using System.IO;
using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.Data.Sqlite;

namespace HotelBooking.Repositories
{
    public class RoomRepository
    {
        private readonly DatabaseContext _db;

        public RoomRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<List<RoomModel>> GetAllAsync()
        {
            var rooms = new List<RoomModel>();
            await using var conn = _db.GetConnection();

            await using var cmd = new SqliteCommand(
                "SELECT RoomNumber, Type, Description, PricePerNight, Capacity, IsAvailable FROM Rooms ORDER BY RoomNumber",
                conn);
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                rooms.Add(new RoomModel
                {
                    RoomNumber    = reader.GetInt32(0),
                    Type          = reader.GetString(1),
                    Description   = reader.GetString(2),
                    PricePerNight = reader.GetDecimal(3),
                    Capacity      = reader.GetInt32(4),
                    IsAvailable   = reader.GetInt32(5) == 1
                });
            }
            return rooms;
        }

        public async Task AddAsync(RoomModel room)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqliteCommand("""
                INSERT INTO Rooms (RoomNumber, Type, Description, PricePerNight, Capacity, IsAvailable)
                VALUES (@rn, @tp, @ds, @pr, @cp, @av)
                """, conn);
            BindRoom(cmd, room);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(RoomModel room)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqliteCommand("""
                UPDATE Rooms SET Type=@tp, Description=@ds, PricePerNight=@pr,
                                 Capacity=@cp, IsAvailable=@av
                WHERE RoomNumber=@rn
                """, conn);
            BindRoom(cmd, room);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int roomNumber)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqliteCommand(
                "DELETE FROM Rooms WHERE RoomNumber=@rn", conn);
            cmd.Parameters.AddWithValue("@rn", roomNumber);
            await cmd.ExecuteNonQueryAsync();
        }

        private static void BindRoom(SqliteCommand cmd, RoomModel r)
        {
            cmd.Parameters.AddWithValue("@rn", r.RoomNumber);
            cmd.Parameters.AddWithValue("@tp", r.Type);
            cmd.Parameters.AddWithValue("@ds", r.Description);
            cmd.Parameters.AddWithValue("@pr", r.PricePerNight);
            cmd.Parameters.AddWithValue("@cp", r.Capacity);
            cmd.Parameters.AddWithValue("@av", r.IsAvailable ? 1 : 0);
        }
    }
}
