using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Task_1.Core;
using Task_1.Models;
using Task_1.Services;

namespace Task_1.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly BookingService _bookingService = new();
        private readonly CommunicationService _commService = new();
        private readonly string _dataPath = "hotel_data.json";
        private readonly string _usersPath = "users.json";

        public ObservableCollection<Room> Rooms { get; } = new();
        public ObservableCollection<Booking> Bookings { get; } = new();

        // День 1/3: Двусторонняя привязка
        private string _guestName;
        public string GuestName { get => _guestName; set { _guestName = value; OnPropertyChanged(); } }

        private DateTime _checkIn = DateTime.Today;
        public DateTime CheckIn { get => _checkIn; set { _checkIn = value; OnPropertyChanged(); } }

        private DateTime _checkOut = DateTime.Today.AddDays(1);
        public DateTime CheckOut { get => _checkOut; set { _checkOut = value; OnPropertyChanged(); } }

        private Room _selectedRoom;
        public Room SelectedRoom { get => _selectedRoom; set { _selectedRoom = value; OnPropertyChanged(); } }

        private bool _isBooking;
        public bool IsBooking { get => _isBooking; set { _isBooking = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanInteract)); } }

        public bool CanInteract => !IsBooking;
        public string StatusMessage { get; private set; } = "Готово к работе";

        // День 2/4: Команды
        public RelayCommand BookCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand CancelCommand { get; }

        public MainViewModel()
        {
            LoadData();
            _commService.StartServerAsync();
            _commService.MessageReceived += msg => Application.Current.Dispatcher.Invoke(() => StatusMessage = $"Чат: {msg}");
            _commService.NotificationReceived += notif => Application.Current.Dispatcher.Invoke(() => MessageBox.Show(notif, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information));

            BookCommand = new RelayCommand(async _ => await BookRoomAsync(), _ => CanInteract && ValidateBooking());
            EditCommand = new RelayCommand(_ => EditBooking(), _ => CanInteract && SelectedBooking != null);
            CancelCommand = new RelayCommand(_ => CancelBooking(), _ => CanInteract && SelectedBooking != null);
        }

        public Booking SelectedBooking { get; set; }

        private async Task BookRoomAsync()
        {
            if (SelectedRoom == null || SelectedRoom.Status != RoomStatus.Free)
            {
                MessageBox.Show("Выберите свободный номер!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            IsBooking = true;
            StatusMessage = "Обработка бронирования...";

            var booking = new Booking
            {
                GuestName = GuestName,
                RoomId = SelectedRoom.Id,
                CheckIn = CheckIn,
                CheckOut = CheckOut
            };

            var success = await _bookingService.ProcessBookingAsync(booking, SelectedRoom);
            if (success)
            {
                Bookings.Add(booking);
                _commService.SendNotification($"Новая бронь: {GuestName} → №{SelectedRoom.Number}");
                StatusMessage = "Бронирование успешно!";
            }

            IsBooking = false;
            SaveData();
        }

        private void EditBooking()
        {
            if (SelectedBooking == null) return;
            GuestName = SelectedBooking.GuestName;
            CheckIn = SelectedBooking.CheckIn;
            CheckOut = SelectedBooking.CheckOut;
            SelectedRoom = Rooms.FirstOrDefault(r => r.Id == SelectedBooking.RoomId);
            StatusMessage = "Режим редактирования";
        }

        private void CancelBooking()
        {
            if (SelectedBooking == null) return;
            var result = MessageBox.Show("Отменить бронирование?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var room = Rooms.First(r => r.Id == SelectedBooking.RoomId);
                _bookingService.CancelBooking(SelectedBooking, room);
                Bookings.Remove(SelectedBooking);
                StatusMessage = "Бронь отменена";
                SaveData();
            }
        }

        private bool ValidateBooking()
        {
            if (string.IsNullOrWhiteSpace(GuestName)) return false;
            if (CheckOut <= CheckIn) return false;
            return true;
        }

        private void LoadData()
        {
            var hotelData = JsonService.Load<HotelData>(_dataPath);
            if (hotelData == null)
            {
                // Демо-данные
                for (int i = 101; i <= 110; i++)
                    Rooms.Add(new Room { Id = i, Number = i, PricePerNight = 2500, Status = RoomStatus.Free });
                var users = new System.Collections.Generic.List<User> { new User { Username = "admin", PasswordHash = "1234", Role = "Manager" } };
                JsonService.Save(_usersPath, users);
            }
            else
            {
                foreach (var r in hotelData.Rooms) Rooms.Add(r);
                foreach (var b in hotelData.Bookings) Bookings.Add(b);
            }
            JsonService.Save(_dataPath, new HotelData { Rooms = Rooms.ToList(), Bookings = Bookings.ToList() });
        }

        private void SaveData()
        {
            JsonService.Save(_dataPath, new HotelData { Rooms = Rooms.ToList(), Bookings = Bookings.ToList() });
        }
    }

    public class HotelData
    {
        public System.Collections.Generic.List<Room> Rooms { get; set; } = new();
        public System.Collections.Generic.List<Booking> Bookings { get; set; } = new();
    }
}