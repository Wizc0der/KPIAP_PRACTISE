using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HotelBooking
{
    public class Room
    {
        public int Number { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }

    public partial class MainWindow : Window
    {
        private List<Room> rooms;

        public MainWindow()
        {
            InitializeComponent();
            LoadRooms();
        }

        private void LoadRooms()
        {
            rooms = new List<Room>
            {
                new Room { Number = 101, Type = "Стандарт", Price = 3000, Status = "Свободен" },
                new Room { Number = 102, Type = "Стандарт", Price = 3000, Status = "Свободен" },
                new Room { Number = 201, Type = "Люкс", Price = 5000, Status = "Свободен" },
                new Room { Number = 202, Type = "Люкс", Price = 5000, Status = "Забронирован" },
                new Room { Number = 301, Type = "Сьют", Price = 8000, Status = "Свободен" }
            };

            dgRooms.ItemsSource = rooms;
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            txtMessage.Text = "";
            txtMessage.Foreground = Brushes.Red;

            if (dgRooms.SelectedItem == null)
            {
                txtMessage.Text = "Выберите номер для бронирования";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                txtMessage.Text = "Введите ФИО";
                return;
            }

            if (!dpCheckIn.SelectedDate.HasValue)
            {
                txtMessage.Text = "Выберите дату заезда";
                return;
            }

            if (!dpCheckOut.SelectedDate.HasValue)
            {
                txtMessage.Text = "Выберите дату выезда";
                return;
            }

            DateTime checkIn = dpCheckIn.SelectedDate.Value;
            DateTime checkOut = dpCheckOut.SelectedDate.Value;

            if (checkIn.Date < DateTime.Today)
            {
                txtMessage.Text = "Дата заезда не может быть в прошлом";
                return;
            }

            if (checkOut <= checkIn)
            {
                txtMessage.Text = "Дата выезда должна быть позже даты заезда";
                return;
            }

            Room selectedRoom = (Room)dgRooms.SelectedItem;

            if (selectedRoom.Status == "Забронирован")
            {
                txtMessage.Text = "Этот номер уже забронирован";
                return;
            }

            int nights = (checkOut.Date - checkIn.Date).Days;
            decimal totalPrice = selectedRoom.Price * nights;

            selectedRoom.Status = "Забронирован";
            dgRooms.Items.Refresh();

            txtMessage.Foreground = (Brush)new BrushConverter().ConvertFromString("#4CAF50");
            txtMessage.Text = $"✓ Бронирование подтверждено!\n" +
                             $"Гость: {txtFullName.Text}\n" +
                             $"Номер: {selectedRoom.Number} ({selectedRoom.Type})\n" +
                             $"Ночей: {nights}\n" +
                             $"Сумма: {totalPrice:C}";

            MessageBox.Show("Бронирование успешно создано!", "Успех",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}