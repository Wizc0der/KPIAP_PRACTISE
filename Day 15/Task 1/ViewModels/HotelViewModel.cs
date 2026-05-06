using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Task_1.Models;
using Task_1.Services;

namespace Task_1.ViewModels
{
    public class HotelViewModel : INotifyPropertyChanged
    {
        private readonly BookingService bookingService;
        private Room selectedRoom;
        private Booking selectedBooking;
        private decimal totalPrice;
        private bool isBusy;
        private string statusMessage;

        public ObservableCollection<Room> Rooms { get; }
        public ObservableCollection<Booking> Bookings { get; }

        public Room SelectedRoom
        {
            get => selectedRoom;
            set { selectedRoom = value; OnPropertyChanged(); CalculateTotal(); }
        }

        public Booking SelectedBooking
        {
            get => selectedBooking;
            set { selectedBooking = value; OnPropertyChanged(); CalculateTotal(); }
        }

        public decimal TotalPrice
        {
            get => totalPrice;
            set { totalPrice = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get => isBusy;
            set { isBusy = value; OnPropertyChanged(); }
        }

        public string StatusMessage
        {
            get => statusMessage;
            set { statusMessage = value; OnPropertyChanged(); }
        }

        public ICommand BookCommand { get; }
        public ICommand CancelCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public HotelViewModel()
        {
            bookingService = new BookingService();
            Rooms = bookingService.GetRooms();
            Bookings = bookingService.GetBookings();

            SelectedBooking = new Booking
            {
                Id = new Random().Next(1000, 9999),
                Status = "Активна",
                CheckIn = DateTime.Today,
                CheckOut = DateTime.Today.AddDays(1)
            };

            BookCommand = new RelayCommand(async () => await BookRoomAsync(), () => !IsBusy);
            CancelCommand = new RelayCommand(async () => await CancelBookingAsync(), () => !IsBusy);
        }

        private void CalculateTotal()
        {
            if (SelectedRoom != null && SelectedBooking?.CheckIn.Date < SelectedBooking.CheckOut.Date)
            {
                int nights = (SelectedBooking.CheckOut.Date - SelectedBooking.CheckIn.Date).Days;
                TotalPrice = SelectedRoom.Price * nights;
            }
            else
            {
                TotalPrice = 0;
            }
        }

        private async Task BookRoomAsync()
        {
            if (SelectedRoom == null)
            {
                StatusMessage = "Выберите номер";
                return;
            }

            IsBusy = true;
            StatusMessage = "Обработка бронирования...";

            var result = await bookingService.BookRoomAsync(SelectedRoom, SelectedBooking);

            IsBusy = false;
            StatusMessage = result.Message;

            if (result.Success)
            {
                SelectedBooking = new Booking
                {
                    Id = new Random().Next(1000, 9999),
                    Status = "Активна",
                    CheckIn = DateTime.Today,
                    CheckOut = DateTime.Today.AddDays(1)
                };
            }
        }

        private async Task CancelBookingAsync()
        {
            if (SelectedRoom == null || SelectedRoom.Status == "Свободен")
            {
                StatusMessage = "Выберите забронированный номер";
                return;
            }

            IsBusy = true;
            StatusMessage = "Отмена бронирования...";

            var result = await bookingService.CancelBookingAsync(SelectedRoom);

            IsBusy = false;
            StatusMessage = result.Message;
        }
    }
}