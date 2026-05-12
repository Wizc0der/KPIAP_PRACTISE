using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Task_1.Models;
using Task_1.ViewModels;

namespace Task_1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            Resources.Add("BoolToVisibilityConverter", new BoolToVisibilityConverter());
            Resources.Add("StatusColorConverter", new StatusColorConverter());
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e) => Close();
    }

    // Конвертеры для XAML
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (bool)value ? Visibility.Visible : Visibility.Collapsed;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }

    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is RoomStatus status ? status == RoomStatus.Free ? "Green" : "Red" : "Gray";
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}