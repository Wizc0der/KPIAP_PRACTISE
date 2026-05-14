using System.IO;
using HotelBooking.Models;
using Newtonsoft.Json;

namespace HotelBooking.Services
{
    public class DataService
    {
        private readonly string _hotelDataPath;
        private readonly string _usersPath;

        public DataService()
        {
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            _hotelDataPath = Path.Combine(appDir, "hotel_data.json");
            _usersPath = Path.Combine(appDir, "users.json");
        }

        public async Task<HotelData> LoadHotelDataAsync()
        {
            if (!File.Exists(_hotelDataPath))
                return GetDefaultHotelData();

            string json = await File.ReadAllTextAsync(_hotelDataPath);
            return JsonConvert.DeserializeObject<HotelData>(json) ?? GetDefaultHotelData();
        }

        public async Task SaveHotelDataAsync(HotelData data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            await File.WriteAllTextAsync(_hotelDataPath, json);
        }

        public async Task<List<UserModel>> LoadUsersAsync()
        {
            if (!File.Exists(_usersPath))
            {
                var defaults = GetDefaultUsers();
                await SaveUsersAsync(defaults);
                return defaults;
            }
            string json = await File.ReadAllTextAsync(_usersPath);
            return JsonConvert.DeserializeObject<List<UserModel>>(json) ?? GetDefaultUsers();
        }

        public async Task SaveUsersAsync(List<UserModel> users)
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            await File.WriteAllTextAsync(_usersPath, json);
        }

        private HotelData GetDefaultHotelData() => new HotelData
        {
            Rooms = new List<RoomModel>
            {
                new() { RoomNumber = 101, Type = "Стандарт", PricePerNight = 85, Capacity = 1, Description = "Уютный одноместный номер с видом на город", IsAvailable = true },
                new() { RoomNumber = 102, Type = "Стандарт", PricePerNight = 110, Capacity = 2, Description = "Двухместный номер с двуспальной кроватью", IsAvailable = true },
                new() { RoomNumber = 201, Type = "Люкс", PricePerNight = 190, Capacity = 2, Description = "Просторный люкс с джакузи и панорамным видом", IsAvailable = true },
                new() { RoomNumber = 202, Type = "Люкс", PricePerNight = 210, Capacity = 3, Description = "Семейный люкс с двумя комнатами", IsAvailable = true },
                new() { RoomNumber = 301, Type = "Президентский", PricePerNight = 420, Capacity = 4, Description = "Эксклюзивный президентский номер, весь этаж", IsAvailable = true },
                new() { RoomNumber = 103, Type = "Стандарт", PricePerNight = 95, Capacity = 2, Description = "Стандартный номер с балконом", IsAvailable = true },
                new() { RoomNumber = 203, Type = "Полулюкс", PricePerNight = 145, Capacity = 2, Description = "Полулюкс с гостиной зоной", IsAvailable = true },
                new() { RoomNumber = 104, Type = "Эконом", PricePerNight = 60, Capacity = 1, Description = "Бюджетный вариант, всё необходимое есть", IsAvailable = true },
            },
            Bookings = new List<BookingModel>(),
            Guests = new List<UserModel>()
        };

        private List<UserModel> GetDefaultUsers() => new List<UserModel>
        {
            new() { Username = "admin", PasswordHash = HashPassword("admin123"), FullName = "Администратор", Role = "Manager", Email = "admin@hotel.ru", Phone = "+7 (999) 000-00-00" }
        };

        public static string HashPassword(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
