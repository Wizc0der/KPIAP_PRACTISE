using System.Windows;
using HotelBooking.Services;
using HotelBooking.ViewModels;

namespace HotelBooking.Views
{
    public partial class LoginWindow : Window
    {
        private readonly AuthViewModel _vm;
        private readonly DataService _dataService;
        private readonly AuthService _authService;

        public LoginWindow()
        {
            InitializeComponent();
            _dataService = new DataService();
            _authService = new AuthService(_dataService);
            _vm = new AuthViewModel(_authService);
            DataContext = _vm;

            _vm.LoginSucceeded += OnLoginSucceeded;
            Loaded += async (_, _) => await _authService.InitializeAsync();
        }

        private void OnLoginSucceeded()
        {
            var main = new MainWindow(_dataService, _authService);
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
