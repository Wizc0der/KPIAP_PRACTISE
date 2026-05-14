using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.Data.Sqlite;

namespace HotelBooking.Repositories
{
    public class BookingRepository
    {
        private readonly DatabaseContext _db;

        public BookingRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<List<BookingModel>> GetAllAsync()
        {
            var list = new List<BookingModel>();
            await using var conn = _db.GetConnection();
            await using var cmd = new SqliteCommand("""
                SELECT Id, RoomNumber, GuestFullName, GuestPhone, GuestEmail,
                       CheckIn, CheckOut, TotalPrice, Status
                FROM Bookings ORDER BY CheckIn DESC
                """, conn);
            await using var r = await cmd.ExecuteReaderAsync();
            while (await r.ReadAsync())
            {
                list.Add(new BookingModel
                {
                    Id            = r.GetString(0),
                    RoomNumber    = r.GetInt32(1),
                    GuestFullName = r.GetString(2),
                    GuestPhone    = r.GetString(3),
                    GuestEmail    = r.GetString(4),
                    CheckIn       = DateTime.Parse(r.GetString(5)),
                    CheckOut      = DateTime.Parse(r.GetString(6)),
                    TotalPrice    = r.GetDecimal(7),
                    Status        = r.GetString(8)
                });
            }
            return list;
        }

        public async Task AddAsync(BookingModel b)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqliteCommand("""
                INSERT INTO Bookings (Id, RoomNumber, GuestFullName, GuestPhone, GuestEmail,
                                     CheckIn, CheckOut, TotalPrice, Status)
                VALUES (@id, @rn, @gn, @gp, @ge, @ci, @co, @tp, @st)
                """, conn);
            BindBooking(cmd, b);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(BookingModel b)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqliteCommand("""
                UPDATE Bookings SET RoomNumber=@rn, GuestFullName=@gn, GuestPhone=@gp,
                    GuestEmail=@ge, CheckIn=@ci, CheckOut=@co, TotalPrice=@tp, Status=@st
                WHERE Id=@id
                """, conn);
            BindBooking(cmd, b);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(string id)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqliteCommand(
                "DELETE FROM Bookings WHERE Id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            await cmd.ExecuteNonQueryAsync();
        }

        private static void BindBooking(SqliteCommand cmd, BookingModel b)
        {
            cmd.Parameters.AddWithValue("@id", b.Id);
            cmd.Parameters.AddWithValue("@rn", b.RoomNumber);
            cmd.Parameters.AddWithValue("@gn", b.GuestFullName);
            cmd.Parameters.AddWithValue("@gp", b.GuestPhone);
            cmd.Parameters.AddWithValue("@ge", b.GuestEmail);
            cmd.Parameters.AddWithValue("@ci", b.CheckIn.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@co", b.CheckOut.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@tp", b.TotalPrice);
            cmd.Parameters.AddWithValue("@st", b.Status);
        }
    }
}
