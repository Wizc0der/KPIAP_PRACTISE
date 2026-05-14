using System.Windows;
using System.Windows.Controls;
using HotelBooking.Repositories;
using HotelBooking.Services;
using HotelBooking.ViewModels;

namespace HotelBooking.Views
{
    public partial class MainWindow : Window
    {
        private readonly HotelViewModel _vm;
        private readonly AuthService    _authService;

        public MainWindow(RoomRepository roomRepo, BookingRepository bookingRepo,
                          BookingService bookingSvc, AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
            _vm = new HotelViewModel(roomRepo, bookingRepo, bookingSvc, authService);
            DataContext = _vm;
            Loaded += async (_, _) => await _vm.LoadDataAsync();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            _authService.Logout();
            var login = new LoginWindow();
            login.Show();
            Close();
        }

        private void RoomsListView_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
    }
}
