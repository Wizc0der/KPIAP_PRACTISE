using System.Windows;
using HotelBooking.Data;
using HotelBooking.Repositories;
using HotelBooking.Services;
using HotelBooking.ViewModels;

namespace HotelBooking.Views
{
    public partial class LoginWindow : Window
    {
        private readonly AuthViewModel   _vm;
        private readonly DatabaseContext _db;
        private readonly AuthService     _authService;

        public LoginWindow()
        {
            InitializeComponent();
            _db          = new DatabaseContext();   // creates hotel.db, seeds data
            _authService = new AuthService(_db);
            _vm          = new AuthViewModel(_authService);
            DataContext  = _vm;
            _vm.LoginSucceeded += OnLoginSucceeded;
        }

        private void OnLoginSucceeded()
        {
            var roomRepo    = new RoomRepository(_db);
            var bookingRepo = new BookingRepository(_db);
            var bookingSvc  = new BookingService();

            var main = new MainWindow(roomRepo, bookingRepo, bookingSvc, _authService);
            main.Show();
            Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.Password = PwdBox.Password;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.Password = RegPwdBox.Password;
        }
    }
}
