using System;
using System.ComponentModel;

namespace HotelBooking.Models
{
    public class BookingModel : INotifyPropertyChanged
    {
        private string _id = Guid.NewGuid().ToString();
        private string _guestFullName = string.Empty;
        private string _guestPhone = string.Empty;
        private string _guestEmail = string.Empty;
        private int _roomNumber;
        private DateTime _checkIn = DateTime.Today;
        private DateTime _checkOut = DateTime.Today.AddDays(1);
        private decimal _totalPrice;
        private string _status = "Активно";

        public string Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string GuestFullName
        {
            get => _guestFullName;
            set { _guestFullName = value; OnPropertyChanged(nameof(GuestFullName)); }
        }

        public string GuestPhone
        {
            get => _guestPhone;
            set { _guestPhone = value; OnPropertyChanged(nameof(GuestPhone)); }
        }

        public string GuestEmail
        {
            get => _guestEmail;
            set { _guestEmail = value; OnPropertyChanged(nameof(GuestEmail)); }
        }

        public int RoomNumber
        {
            get => _roomNumber;
            set { _roomNumber = value; OnPropertyChanged(nameof(RoomNumber)); }
        }

        public DateTime CheckIn
        {
            get => _checkIn;
            set { _checkIn = value; OnPropertyChanged(nameof(CheckIn)); OnPropertyChanged(nameof(Nights)); }
        }

        public DateTime CheckOut
        {
            get => _checkOut;
            set { _checkOut = value; OnPropertyChanged(nameof(CheckOut)); OnPropertyChanged(nameof(Nights)); }
        }

        public int Nights => (CheckOut - CheckIn).Days > 0 ? (CheckOut - CheckIn).Days : 0;

        public decimal TotalPrice
        {
            get => _totalPrice;
            set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }

        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
