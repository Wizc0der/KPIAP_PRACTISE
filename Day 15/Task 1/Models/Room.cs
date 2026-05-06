using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Task_1.Models
{
    public class Room : INotifyPropertyChanged
    {
        private int number;
        private string type;
        private decimal price;
        private string status;

        public int Number { get => number; set { number = value; OnPropertyChanged(); } }
        public string Type { get => type; set { type = value; OnPropertyChanged(); } }
        public decimal Price { get => price; set { price = value; OnPropertyChanged(); } }
        public string Status { get => status; set { status = value; OnPropertyChanged(); } }

        public string DisplayText => $"№{Number} - {Type} ({Price:0} ₽)";

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}