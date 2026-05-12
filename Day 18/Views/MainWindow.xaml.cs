using System.Windows;
using System.Windows.Controls;
using HotelBooking.Services;
using HotelBooking.ViewModels;

namespace HotelBooking.Views
{
    public partial class MainWindow : Window
    {
        private readonly HotelViewModel _vm;
        private readonly AuthService _authService;

        public MainWindow(DataService dataService, AuthService authService)
        {
            InitializeComponent();
            _authService = authService;

            var bookingService = new BookingService(dataService);
            _vm = new HotelViewModel(dataService, bookingService, authService);
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

        private void RoomsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Animate the selected item with a press effect - handled via DataTemplate triggers
            // ViewModel binding handles the rest
        }
    }
}
