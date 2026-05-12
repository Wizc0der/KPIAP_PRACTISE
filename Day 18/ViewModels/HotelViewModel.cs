using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HotelBooking.Commands;
using HotelBooking.Models;
using HotelBooking.Services;

namespace HotelBooking.ViewModels
{
    public class HotelViewModel : BaseViewModel
    {
        private readonly DataService _dataService;
        private readonly BookingService _bookingService;
        private readonly AuthService _authService;
        private HotelData _hotelData = new();

        // ── State ──────────────────────────────────────────────
        private bool _isBusy;
        private string _busyMessage = string.Empty;
        private string _statusMessage = string.Empty;
        private RoomModel? _selectedRoom;
        private BookingModel? _selectedBooking;
        private string _filterType = "Все";
        private bool _showOnlyAvailable;
        private string _searchText = string.Empty;

        // ── Booking form fields (TwoWay) ───────────────────────
        private string _guestFullName = string.Empty;
        private string _guestPhone = string.Empty;
        private string _guestEmail = string.Empty;
        private DateTime _checkIn = DateTime.Today;
        private DateTime _checkOut = DateTime.Today.AddDays(1);

        // ── Collections ────────────────────────────────────────
        public ObservableCollection<RoomModel> Rooms { get; } = new();
        public ObservableCollection<BookingModel> Bookings { get; } = new();
        public ObservableCollection<string> RoomTypes { get; } = new() { "Все", "Эконом", "Стандарт", "Полулюкс", "Люкс", "Президентский" };

        // ── Properties ─────────────────────────────────────────
        public bool IsBusy { get => _isBusy; set => SetField(ref _isBusy, value); }
        public string BusyMessage { get => _busyMessage; set => SetField(ref _busyMessage, value); }
        public string StatusMessage { get => _statusMessage; set => SetField(ref _statusMessage, value); }

        public RoomModel? SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                SetField(ref _selectedRoom, value);
                OnPropertyChanged(nameof(SelectedRoomInfo));
                OnPropertyChanged(nameof(IsRoomSelected));
                OnPropertyChanged(nameof(EstimatedTotal));
            }
        }

        public BookingModel? SelectedBooking
        {
            get => _selectedBooking;
            set { SetField(ref _selectedBooking, value); OnPropertyChanged(nameof(IsBookingSelected)); }
        }

        public bool IsRoomSelected => SelectedRoom != null;
        public bool IsBookingSelected => SelectedBooking != null;

        public string SelectedRoomInfo => SelectedRoom == null ? "Выберите номер из списка"
            : $"№{SelectedRoom.RoomNumber} | {SelectedRoom.Type} | {SelectedRoom.PricePerNight:N0} Br/ночь | Вместимость: {SelectedRoom.Capacity} чел.\n{SelectedRoom.Description}";

        public string EstimatedTotal
        {
            get
            {
                if (SelectedRoom == null) return "—";
                int nights = (CheckOut - CheckIn).Days;
                if (nights <= 0) return "—";
                return $"{SelectedRoom.PricePerNight * nights:N0} Br ({nights} ночей)";
            }
        }

        // TwoWay bindings for guest form
        public string GuestFullName { get => _guestFullName; set { SetField(ref _guestFullName, value); OnPropertyChanged(nameof(EstimatedTotal)); } }
        public string GuestPhone { get => _guestPhone; set => SetField(ref _guestPhone, value); }
        public string GuestEmail { get => _guestEmail; set => SetField(ref _guestEmail, value); }

        public DateTime CheckIn
        {
            get => _checkIn;
            set { SetField(ref _checkIn, value); OnPropertyChanged(nameof(EstimatedTotal)); }
        }

        public DateTime CheckOut
        {
            get => _checkOut;
            set { SetField(ref _checkOut, value); OnPropertyChanged(nameof(EstimatedTotal)); }
        }

        public string FilterType { get => _filterType; set { SetField(ref _filterType, value); ApplyFilter(); } }
        public bool ShowOnlyAvailable { get => _showOnlyAvailable; set { SetField(ref _showOnlyAvailable, value); ApplyFilter(); } }
        public string SearchText { get => _searchText; set { SetField(ref _searchText, value); ApplyFilter(); } }

        public string ManagerName => _authService.CurrentUser?.FullName ?? "Менеджер";

        // ── Commands ───────────────────────────────────────────
        public ICommand BookRoomCommand { get; }
        public ICommand CancelBookingCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand ClearFormCommand { get; }

        // ── Constructor ────────────────────────────────────────
        public HotelViewModel(DataService dataService, BookingService bookingService, AuthService authService)
        {
            _dataService = dataService;
            _bookingService = bookingService;
            _authService = authService;

            BookRoomCommand = new AsyncRelayCommand(BookRoomAsync, _ => !IsBusy && SelectedRoom?.IsAvailable == true);
            CancelBookingCommand = new AsyncRelayCommand(CancelBookingAsync, _ => !IsBusy && SelectedBooking?.Status == "Активно");
            RefreshCommand = new AsyncRelayCommand(_ => LoadDataAsync());
            ClearFormCommand = new RelayCommand(_ => ClearForm());
        }

        public async Task LoadDataAsync()
        {
            IsBusy = true;
            BusyMessage = "Загрузка данных...";
            try
            {
                _hotelData = await _dataService.LoadHotelDataAsync();
                Rooms.Clear();
                foreach (var r in _hotelData.Rooms) Rooms.Add(r);

                Bookings.Clear();
                foreach (var b in _hotelData.Bookings.OrderByDescending(x => x.CheckIn)) Bookings.Add(b);

                StatusMessage = $"Загружено {Rooms.Count} номеров, {Bookings.Count} бронирований.";
            }
            finally { IsBusy = false; }
        }

        private async Task BookRoomAsync(object? _)
        {
            if (SelectedRoom == null) return;

            var booking = new BookingModel
            {
                GuestFullName = GuestFullName,
                GuestPhone = GuestPhone,
                GuestEmail = GuestEmail,
                CheckIn = CheckIn,
                CheckOut = CheckOut,
                RoomNumber = SelectedRoom.RoomNumber
            };

            var (valid, error) = _bookingService.ValidateBooking(booking);
            if (!valid) { StatusMessage = $"❌ {error}"; return; }

            IsBusy = true;
            BusyMessage = "Отправка запроса на бронирование...";
            try
            {
                // Register guest automatically
                await _authService.RegisterGuestAsync(_hotelData, GuestFullName, GuestPhone, GuestEmail);

                var (success, message) = await _bookingService.BookRoomAsync(SelectedRoom, booking, _hotelData);
                StatusMessage = success ? $"✅ {message}" : $"❌ {message}";

                if (success)
                {
                    Bookings.Insert(0, booking);
                    ClearForm();
                    OnPropertyChanged(nameof(SelectedRoomInfo));
                }
            }
            finally { IsBusy = false; }
        }

        private async Task CancelBookingAsync(object? _)
        {
            if (SelectedBooking == null) return;

            IsBusy = true;
            BusyMessage = "Отмена бронирования...";
            try
            {
                var (success, message) = await _bookingService.CancelBookingAsync(SelectedBooking, _hotelData);
                StatusMessage = success ? $"✅ {message}" : $"❌ {message}";
                if (success)
                {
                    // Update room in UI
                    var room = Rooms.FirstOrDefault(r => r.RoomNumber == SelectedBooking.RoomNumber);
                    if (room != null) room.IsAvailable = true;
                    OnPropertyChanged(nameof(SelectedRoomInfo));
                }
            }
            finally { IsBusy = false; }
        }

        private void ApplyFilter()
        {
            var filtered = _hotelData.Rooms.AsEnumerable();
            if (FilterType != "Все") filtered = filtered.Where(r => r.Type == FilterType);
            if (ShowOnlyAvailable) filtered = filtered.Where(r => r.IsAvailable);
            if (!string.IsNullOrWhiteSpace(SearchText))
                filtered = filtered.Where(r => r.RoomNumber.ToString().Contains(SearchText) ||
                                               r.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            Rooms.Clear();
            foreach (var r in filtered) Rooms.Add(r);
        }

        private void ClearForm()
        {
            GuestFullName = string.Empty;
            GuestPhone = string.Empty;
            GuestEmail = string.Empty;
            CheckIn = DateTime.Today;
            CheckOut = DateTime.Today.AddDays(1);
            SelectedRoom = null;
        }
    }
}
