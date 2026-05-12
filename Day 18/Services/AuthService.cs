using HotelBooking.Models;

namespace HotelBooking.Services
{
    public class AuthService
    {
        private readonly DataService _dataService;
        private List<UserModel> _users = new();
        public UserModel? CurrentUser { get; private set; }

        public AuthService(DataService dataService)
        {
            _dataService = dataService;
        }

        public async Task InitializeAsync()
        {
            _users = await _dataService.LoadUsersAsync();
        }

        public (bool Success, string Message) Login(string username, string password)
        {
            var hash = DataService.HashPassword(password);
            var user = _users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.PasswordHash == hash &&
                u.Role == "Manager");

            if (user == null)
                return (false, "Неверный логин или пароль.");

            CurrentUser = user;
            return (true, $"Добро пожаловать, {user.FullName}!");
        }

        public async Task<(bool Success, string Message)> RegisterManagerAsync(
            string username, string password, string fullName, string email, string phone)
        {
            if (_users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                return (false, "Пользователь с таким логином уже существует.");

            if (password.Length < 6)
                return (false, "Пароль должен содержать минимум 6 символов.");

            var newUser = new UserModel
            {
                Username = username,
                PasswordHash = DataService.HashPassword(password),
                FullName = fullName,
                Email = email,
                Phone = phone,
                Role = "Manager"
            };
            _users.Add(newUser);
            await _dataService.SaveUsersAsync(_users);
            return (true, "Менеджер успешно зарегистрирован.");
        }

        public async Task<UserModel> RegisterGuestAsync(HotelData hotelData, string fullName, string phone, string email)
        {
            var existing = hotelData.Guests.FirstOrDefault(g =>
                g.FullName.Equals(fullName, StringComparison.OrdinalIgnoreCase) && g.Phone == phone);

            if (existing != null) return existing;

            var guest = new UserModel
            {
                FullName = fullName,
                Phone = phone,
                Email = email,
                Role = "Guest",
                Username = phone
            };
            hotelData.Guests.Add(guest);
            await _dataService.SaveHotelDataAsync(hotelData);
            return guest;
        }

        public void Logout() => CurrentUser = null;
    }
}
