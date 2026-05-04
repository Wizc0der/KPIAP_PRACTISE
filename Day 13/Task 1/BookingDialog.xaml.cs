using System;
using System.Windows;

namespace Task_1
{
    public partial class BookingDialog : Window
    {
        public Booking Booking { get; private set; }

        public BookingDialog(Booking existing = null, bool isEdit = false)
        {
            InitializeComponent();

            cmbRoom.ItemsSource = new[] { 101, 102, 201, 202, 301 };

            if (isEdit && existing != null)
            {
                Title = "Редактирование брони";
                Booking = existing;
                txtFullName.Text = existing.GuestName;
                cmbRoom.SelectedValue = existing.RoomNumber;
                dpCheckIn.SelectedDate = existing.CheckIn;
                dpCheckOut.SelectedDate = existing.CheckOut;
            }
            else
            {
                Booking = new Booking { Id = new Random().Next(1000, 9999), Status = "Активна" };
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Введите ФИО гостя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cmbRoom.SelectedValue == null)
            {
                MessageBox.Show("Выберите номер", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!dpCheckIn.SelectedDate.HasValue || !dpCheckOut.SelectedDate.HasValue)
            {
                MessageBox.Show("Выберите даты заезда и выезда", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Booking.GuestName = txtFullName.Text;
            Booking.RoomNumber = (int)cmbRoom.SelectedValue;
            Booking.CheckIn = dpCheckIn.SelectedDate.Value;
            Booking.CheckOut = dpCheckOut.SelectedDate.Value;
            Booking.TotalPrice = (Booking.CheckOut - Booking.CheckIn).Days * 3000;

            DialogResult = true;
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}