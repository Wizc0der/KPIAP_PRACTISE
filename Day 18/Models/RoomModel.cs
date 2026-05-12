using System.ComponentModel;

namespace HotelBooking.Models
{
    public class RoomModel : INotifyPropertyChanged
    {
        private int _roomNumber;
        private string _type = string.Empty;
        private decimal _pricePerNight;
        private bool _isAvailable = true;
        private string _description = string.Empty;
        private int _capacity;

        public int RoomNumber
        {
            get => _roomNumber;
            set { _roomNumber = value; OnPropertyChanged(nameof(RoomNumber)); }
        }

        public string Type
        {
            get => _type;
            set { _type = value; OnPropertyChanged(nameof(Type)); }
        }

        public decimal PricePerNight
        {
            get => _pricePerNight;
            set { _pricePerNight = value; OnPropertyChanged(nameof(PricePerNight)); }
        }

        public bool IsAvailable
        {
            get => _isAvailable;
            set { _isAvailable = value; OnPropertyChanged(nameof(IsAvailable)); OnPropertyChanged(nameof(StatusText)); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        public int Capacity
        {
            get => _capacity;
            set { _capacity = value; OnPropertyChanged(nameof(Capacity)); }
        }

        public string StatusText => IsAvailable ? "Свободен" : "Занят";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
