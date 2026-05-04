using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Task_1
{
    public class Booking
    {
        public int Id { get; set; }
        public string GuestName { get; set; }
        public int RoomNumber { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
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

    public partial class MainWindow : Window
    {
        private List<Booking> bookings;

        public RelayCommand BookCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand CancelCommand { get; }

        public MainWindow()
        {
            InitializeComponent();

            BookCommand = new RelayCommand(BookRoom);
            EditCommand = new RelayCommand(EditBooking);
            CancelCommand = new RelayCommand(CancelBooking);

            DataContext = this;
            LoadBookings();
        }

        private void LoadBookings()
        {
            bookings = new List<Booking>
            {
                new Booking { Id = 1, GuestName = "Иванов А.А.", RoomNumber = 101, CheckIn = DateTime.Today, CheckOut = DateTime.Today.AddDays(3), TotalPrice = 9000, Status = "Активна" },
                new Booking { Id = 2, GuestName = "Петров В.В.", RoomNumber = 201, CheckIn = DateTime.Today.AddDays(1), CheckOut = DateTime.Today.AddDays(5), TotalPrice = 20000, Status = "Активна" },
                new Booking { Id = 3, GuestName = "Сидоров С.С.", RoomNumber = 301, CheckIn = DateTime.Today.AddDays(-2), CheckOut = DateTime.Today, TotalPrice = 16000, Status = "Отменена" }
            };
            dgBookings.ItemsSource = bookings;
        }

        private void BookRoom()
        {
            var dialog = new BookingDialog();
            if (dialog.ShowDialog() == true)
            {
                bookings.Add(dialog.Booking);
                dgBookings.Items.Refresh();
                txtStatus.Text = $"Бронь №{dialog.Booking.Id} создана";
            }
        }

        private void EditBooking()
        {
            if (dgBookings.SelectedItem == null)
            {
                MessageBox.Show("Выберите бронь для редактирования", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var booking = (Booking)dgBookings.SelectedItem;
            if (booking.Status == "Отменена")
            {
                MessageBox.Show("Нельзя редактировать отменённую бронь", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var dialog = new BookingDialog(booking, true);
            if (dialog.ShowDialog() == true)
            {
                dgBookings.Items.Refresh();
                txtStatus.Text = $"Бронь №{booking.Id} обновлена";
            }
        }

        private void CancelBooking()
        {
            if (dgBookings.SelectedItem == null)
            {
                MessageBox.Show("Выберите бронь для отмены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var booking = (Booking)dgBookings.SelectedItem;
            if (booking.Status == "Отменена")
            {
                MessageBox.Show("Бронь уже отменена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show($"Отменить бронь №{booking.Id}?\nГость: {booking.GuestName}", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                booking.Status = "Отменена";
                dgBookings.Items.Refresh();
                txtStatus.Text = $"Бронь №{booking.Id} отменена";
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e) => Close();
        private void ShowRoomsClick(object sender, RoutedEventArgs e) => txtStatus.Text = "Переход к списку номеров...";
        private void AddRoomClick(object sender, RoutedEventArgs e) => txtStatus.Text = "Добавление номера...";
        private void BookClick(object sender, RoutedEventArgs e) => BookCommand.Execute(null);
        private void EditClick(object sender, RoutedEventArgs e) => EditCommand.Execute(null);
        private void CancelClick(object sender, RoutedEventArgs e) => CancelCommand.Execute(null);
        private void AboutClick(object sender, RoutedEventArgs e) => MessageBox.Show("Hotel Booking System v1.0\n© 2026", "О программе");
    }
}