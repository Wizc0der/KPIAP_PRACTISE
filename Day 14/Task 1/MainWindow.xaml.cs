using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Task_1
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

    public class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => canExecute?.Invoke() ?? true;
        public void Execute(object parameter) => execute();
    }

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Room> rooms;
        private ObservableCollection<Booking> bookings;
        private Room selectedRoom;
        private Booking selectedBooking;
        private decimal totalPrice;

        public ObservableCollection<Room> Rooms { get => rooms; set { rooms = value; OnPropertyChanged(); } }
        public Room SelectedRoom { get => selectedRoom; set { selectedRoom = value; OnPropertyChanged(); CalculateTotal(); } }
        public Booking SelectedBooking { get => selectedBooking; set { selectedBooking = value; OnPropertyChanged(); CalculateTotal(); } }
        public decimal TotalPrice { get => totalPrice; set { totalPrice = value; OnPropertyChanged(); } }

        public RelayCommand BookCommand { get; }
        public RelayCommand CancelCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public MainWindow()
        {
            InitializeComponent();

            SelectedBooking = new Booking
            {
                Id = new Random().Next(1000, 9999),
                Status = "Активна",
                CheckIn = DateTime.Today,
                CheckOut = DateTime.Today.AddDays(1)
            };

            BookCommand = new RelayCommand(BookRoom, () => SelectedRoom?.Status == "Свободен");
            CancelCommand = new RelayCommand(CancelBooking);

            DataContext = this;
            LoadData();
        }

        private void LoadData()
        {
            Rooms = new ObservableCollection<Room>
            {
                new Room { Number = 101, Type = "Стандарт", Price = 3000, Status = "Свободен" },
                new Room { Number = 102, Type = "Стандарт", Price = 3000, Status = "Свободен" },
                new Room { Number = 103, Type = "Стандарт", Price = 3000, Status = "Свободен" },
                new Room { Number = 104, Type = "Стандарт", Price = 3000, Status = "Забронирован" },
                new Room { Number = 201, Type = "Люкс", Price = 5000, Status = "Свободен" },
                new Room { Number = 202, Type = "Люкс", Price = 5000, Status = "Забронирован" },
                new Room { Number = 203, Type = "Люкс", Price = 5000, Status = "Свободен" },
                new Room { Number = 204, Type = "Люкс", Price = 5000, Status = "Свободен" },
                new Room { Number = 301, Type = "Сьют", Price = 8000, Status = "Свободен" },
                new Room { Number = 302, Type = "Сьют", Price = 8000, Status = "Свободен" },
                new Room { Number = 303, Type = "Сьют", Price = 8000, Status = "Забронирован" },
                new Room { Number = 401, Type = "Президентский", Price = 15000, Status = "Свободен" }
            };

            bookings = new ObservableCollection<Booking>();
            icRooms.ItemsSource = Rooms;
        }

        private void CalculateTotal()
        {
            if (SelectedRoom != null && SelectedBooking.CheckIn.Date < SelectedBooking.CheckOut.Date)
            {
                int nights = (SelectedBooking.CheckOut.Date - SelectedBooking.CheckIn.Date).Days;
                TotalPrice = SelectedRoom.Price * nights;
            }
            else
            {
                TotalPrice = 0;
            }
        }

        private void BookRoom()
        {
            if (SelectedRoom == null)
            {
                MessageBox.Show("Выберите номер", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(SelectedBooking.GuestName))
            {
                MessageBox.Show("Введите ФИО гостя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedBooking.CheckIn >= SelectedBooking.CheckOut)
            {
                MessageBox.Show("Дата выезда должна быть позже заезда", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedRoom.Status = "Забронирован";
            SelectedBooking.TotalPrice = TotalPrice;
            SelectedBooking.RoomNumber = SelectedRoom.Number;
            bookings.Add(SelectedBooking);

            MessageBox.Show($"Бронь №{SelectedBooking.Id} создана!\nСумма: {TotalPrice:0} ₽", "Успех",
                          MessageBoxButton.OK, MessageBoxImage.Information);

            SelectedBooking = new Booking
            {
                Id = new Random().Next(1000, 9999),
                Status = "Активна",
                CheckIn = DateTime.Today,
                CheckOut = DateTime.Today.AddDays(1)
            };
        }

        private void CancelBooking()
        {
            if (SelectedRoom == null || SelectedRoom.Status == "Свободен")
            {
                MessageBox.Show("Выберите забронированный номер", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Отменить бронь номера №{SelectedRoom.Number}?", "Подтверждение",
                                        MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SelectedRoom.Status = "Свободен";
                MessageBox.Show("Бронь отменена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e) => Close();
        private void BookClick(object sender, RoutedEventArgs e) => BookCommand.Execute(null);
        private void CancelClick(object sender, RoutedEventArgs e) => CancelCommand.Execute(null);
        private void AboutClick(object sender, RoutedEventArgs e) => MessageBox.Show("Hotel Booking System v1.0\n© 2026", "О программе");
    }
}