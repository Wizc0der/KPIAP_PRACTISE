using HotelBooking.Commands;
using HotelBooking.Services;
using System.Windows.Input;

namespace HotelBooking.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isRegisterMode;
        private string _regFullName = string.Empty;
        private string _regEmail = string.Empty;
        private string _regPhone = string.Empty;
        private bool _isBusy;

        public string Username { get => _username; set => SetField(ref _username, value); }
        public string Password { get => _password; set => SetField(ref _password, value); }
        public string ErrorMessage { get => _errorMessage; set => SetField(ref _errorMessage, value); }
        public bool IsRegisterMode { get => _isRegisterMode; set { SetField(ref _isRegisterMode, value); OnPropertyChanged(nameof(LoginMode)); } }
        public bool LoginMode => !_isRegisterMode;
        public bool IsBusy { get => _isBusy; set => SetField(ref _isBusy, value); }

        public string RegFullName { get => _regFullName; set => SetField(ref _regFullName, value); }
        public string RegEmail { get => _regEmail; set => SetField(ref _regEmail, value); }
        public string RegPhone { get => _regPhone; set => SetField(ref _regPhone, value); }

        public event Action? LoginSucceeded;

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ToggleModeCommand { get; }

        public AuthViewModel(AuthService authService)
        {
            _authService = authService;
            LoginCommand = new RelayCommand(_ => DoLogin());
            RegisterCommand = new AsyncRelayCommand(_ => DoRegisterAsync());
            ToggleModeCommand = new RelayCommand(_ => { IsRegisterMode = !IsRegisterMode; ErrorMessage = string.Empty; });
        }

        private void DoLogin()
        {
            ErrorMessage = string.Empty;
            var (success, message) = _authService.Login(Username, Password);
            if (success) LoginSucceeded?.Invoke();
            else ErrorMessage = message;
        }

        private async Task DoRegisterAsync()
        {
            ErrorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(RegFullName)) { ErrorMessage = "Введите ФИО."; return; }
            IsBusy = true;
            try
            {
                var (success, message) = await _authService.RegisterManagerAsync(Username, Password, RegFullName, RegEmail, RegPhone);
                ErrorMessage = success ? "✅ " + message + "\nВойдите с новыми данными." : message;
                if (success) IsRegisterMode = false;
            }
            finally { IsBusy = false; }
        }
    }
}
