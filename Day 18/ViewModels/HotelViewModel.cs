using System.Collections.ObjectModel;
using System.Windows.Input;
using HotelBooking.Commands;
using HotelBooking.Models;
using HotelBooking.Repositories;
using HotelBooking.Services;

namespace HotelBooking.ViewModels
{
    public class HotelViewModel : BaseViewModel
    {
        private readonly RoomRepository    _roomRepo;
        private readonly BookingRepository _bookingRepo;
        private readonly BookingService    _bookingService;
        private readonly AuthService       _authService;

        // ── State ──────────────────────────────────────────────────
        private bool          _isBusy;
        private string        _busyMessage   = string.Empty;
        private string        _statusMessage = string.Empty;
        private RoomModel?    _selectedRoom;
        private BookingModel? _selectedBooking;
        private string        _filterType    = "Все";
        private bool          _showOnlyAvailable;
        private string        _searchText    = string.Empty;

        // ── Booking form (TwoWay) ──────────────────────────────────
        private string   _guestFullName = string.Empty;
        private string   _guestPhone    = string.Empty;
        private string   _guestEmail    = string.Empty;
        private DateTime _checkIn       = DateTime.Today;
        private DateTime _checkOut      = DateTime.Today.AddDays(1);

        // Local cache for filtering
        private List<RoomModel> _allRooms = new();

        // ── Collections ────────────────────────────────────────────
        public ObservableCollection<RoomModel>    Rooms    { get; } = new();
        public ObservableCollection<BookingModel> Bookings { get; } = new();
        public ObservableCollection<string> RoomTypes { get; } =
            new() { "Все", "Эконом", "Стандарт", "Полулюкс", "Люкс", "Президентский" };

        // ── Properties ─────────────────────────────────────────────
        public bool   IsBusy        { get => _isBusy;        set => SetField(ref _isBusy, value); }
        public string BusyMessage   { get => _busyMessage;   set => SetField(ref _busyMessage, value); }
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
            set
            {
                SetField(ref _selectedBooking, value);
                OnPropertyChanged(nameof(IsBookingSelected));
            }
        }

        public bool IsRoomSelected    => SelectedRoom != null;
        public bool IsBookingSelected => SelectedBooking?.Status == "Активно";

        public string SelectedRoomInfo => SelectedRoom == null
            ? "Выберите номер из списка"
            : $"№{SelectedRoom.RoomNumber} | {SelectedRoom.Type} | {SelectedRoom.PricePerNight:N0} BYN/ночь | {SelectedRoom.Capacity} чел.\n{SelectedRoom.Description}";

        public string EstimatedTotal
        {
            get
            {
                if (SelectedRoom == null) return "—";
                int nights = (CheckOut - CheckIn).Days;
                if (nights <= 0) return "—";
                return $"{SelectedRoom.PricePerNight * nights:N0} BYN ({nights} ночей)";
            }
        }

        // TwoWay bindings
        public string GuestFullName
        {
            get => _guestFullName;
            set { SetField(ref _guestFullName, value); OnPropertyChanged(nameof(EstimatedTotal)); }
        }
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

        public string FilterType
        {
            get => _filterType;
            set { SetField(ref _filterType, value); ApplyFilter(); }
        }
        public bool ShowOnlyAvailable
        {
            get => _showOnlyAvailable;
            set { SetField(ref _showOnlyAvailable, value); ApplyFilter(); }
        }
        public string SearchText
        {
            get => _searchText;
            set { SetField(ref _searchText, value); ApplyFilter(); }
        }

        public string ManagerName => _authService.CurrentUser?.FullName ?? "Менеджер";

        // ── Commands ───────────────────────────────────────────────
        public ICommand BookRoomCommand      { get; }
        public ICommand CancelBookingCommand { get; }
        public ICommand RefreshCommand       { get; }
        public ICommand ClearFormCommand     { get; }

        // ── Constructor ────────────────────────────────────────────
        public HotelViewModel(RoomRepository roomRepo, BookingRepository bookingRepo,
                              BookingService bookingService, AuthService authService)
        {
            _roomRepo       = roomRepo;
            _bookingRepo    = bookingRepo;
            _bookingService = bookingService;
            _authService    = authService;

            BookRoomCommand      = new AsyncRelayCommand(
                ExecuteBookRoomAsync,
                _ => !IsBusy && SelectedRoom?.IsAvailable == true);

            CancelBookingCommand = new AsyncRelayCommand(
                ExecuteCancelBookingAsync,
                _ => !IsBusy && IsBookingSelected);

            RefreshCommand   = new AsyncRelayCommand(_ => LoadDataAsync());
            ClearFormCommand = new RelayCommand(_ => ClearForm());
        }

        // ── Load all data from SQLite ──────────────────────────────
        public async Task LoadDataAsync()
        {
            IsBusy = true;
            BusyMessage = "Загрузка данных из базы...";
            try
            {
                _allRooms = await _roomRepo.GetAllAsync();
                ApplyFilter();

                var bookings = await _bookingRepo.GetAllAsync();
                Bookings.Clear();
                foreach (var b in bookings) Bookings.Add(b);

                StatusMessage = $"Загружено {_allRooms.Count} номеров, {Bookings.Count} бронирований.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"❌ Ошибка загрузки: {ex.Message}";
            }
            finally { IsBusy = false; }
        }

        // ── Book room ──────────────────────────────────────────────
        private async Task ExecuteBookRoomAsync(object? _)
        {
            if (SelectedRoom == null) return;

            var booking = new BookingModel
            {
                Id            = Guid.NewGuid().ToString(),
                GuestFullName = GuestFullName,
                GuestPhone    = GuestPhone,
                GuestEmail    = GuestEmail,
                CheckIn       = CheckIn,
                CheckOut      = CheckOut,
                RoomNumber    = SelectedRoom.RoomNumber,
                TotalPrice    = SelectedRoom.PricePerNight * Math.Max(1, (CheckOut - CheckIn).Days),
                Status        = "Активно"
            };

            var (valid, error) = _bookingService.ValidateBooking(booking);
            if (!valid) { StatusMessage = $"❌ {error}"; return; }

            if (!SelectedRoom.IsAvailable) { StatusMessage = "❌ Номер уже занят."; return; }

            IsBusy = true;
            BusyMessage = "Отправка запроса на бронирование...";
            try
            {
                // Auto-register guest
                await _authService.RegisterGuestAsync(GuestFullName, GuestPhone, GuestEmail);

                // Simulate server confirmation
                await Task.Delay(3000);

                // Save booking to DB
                await _bookingRepo.AddAsync(booking);

                // Mark room as unavailable in DB
                SelectedRoom.IsAvailable = false;
                await _roomRepo.UpdateAsync(SelectedRoom);

                Bookings.Insert(0, booking);
                StatusMessage = $"✅ Номер {SelectedRoom.RoomNumber} забронирован! Сумма: {booking.TotalPrice:N0} BYN";
                OnPropertyChanged(nameof(SelectedRoomInfo));
                ClearForm();
            }
            catch (Exception ex)
            {
                StatusMessage = $"❌ Ошибка: {ex.Message}";
            }
            finally { IsBusy = false; }
        }

        // ── Cancel booking ─────────────────────────────────────────
        private async Task ExecuteCancelBookingAsync(object? _)
        {
            if (SelectedBooking == null) return;

            IsBusy = true;
            BusyMessage = "Отмена бронирования...";
            try
            {
                await Task.Delay(1500);

                SelectedBooking.Status = "Отменено";
                await _bookingRepo.UpdateAsync(SelectedBooking);

                // Free room
                var room = _allRooms.FirstOrDefault(r => r.RoomNumber == SelectedBooking.RoomNumber);
                if (room != null)
                {
                    room.IsAvailable = true;
                    await _roomRepo.UpdateAsync(room);
                }

                StatusMessage = $"✅ Бронирование номера {SelectedBooking.RoomNumber} отменено.";
                OnPropertyChanged(nameof(IsBookingSelected));
                OnPropertyChanged(nameof(SelectedRoomInfo));
            }
            catch (Exception ex)
            {
                StatusMessage = $"❌ Ошибка: {ex.Message}";
            }
            finally { IsBusy = false; }
        }

        // ── Filter rooms ───────────────────────────────────────────
        private void ApplyFilter()
        {
            var filtered = _allRooms.AsEnumerable();

            if (FilterType != "Все")
                filtered = filtered.Where(r => r.Type == FilterType);
            if (ShowOnlyAvailable)
                filtered = filtered.Where(r => r.IsAvailable);
            if (!string.IsNullOrWhiteSpace(SearchText))
                filtered = filtered.Where(r =>
                    r.RoomNumber.ToString().Contains(SearchText) ||
                    r.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            Rooms.Clear();
            foreach (var r in filtered) Rooms.Add(r);
        }

        private void ClearForm()
        {
            GuestFullName = string.Empty;
            GuestPhone    = string.Empty;
            GuestEmail    = string.Empty;
            CheckIn       = DateTime.Today;
            CheckOut      = DateTime.Today.AddDays(1);
            SelectedRoom  = null;
        }
    }
}
