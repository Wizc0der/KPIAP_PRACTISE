using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Task_1.Models
{
    public class Booking : INotifyPropertyChanged
    {
        private int id;
        private int roomNumber;
        private string guestName;
        private string phone;
        private DateTime checkIn;
        private DateTime checkOut;
        private decimal totalPrice;
        private string status;

        public int Id { get => id; set { id = value; OnPropertyChanged(); } }
        public int RoomNumber { get => roomNumber; set { roomNumber = value; OnPropertyChanged(); } }
        public string GuestName { get => guestName; set { guestName = value; OnPropertyChanged(); } }
        public string Phone { get => phone; set { phone = value; OnPropertyChanged(); } }
        public DateTime CheckIn { get => checkIn; set { checkIn = value; OnPropertyChanged(); } }
        public DateTime CheckOut { get => checkOut; set { checkOut = value; OnPropertyChanged(); } }
        public decimal TotalPrice { get => totalPrice; set { totalPrice = value; OnPropertyChanged(); } }
        public string Status { get => status; set { status = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}