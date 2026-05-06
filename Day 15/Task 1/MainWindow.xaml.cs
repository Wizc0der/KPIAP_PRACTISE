using System.Windows;
using Task_1.ViewModels;

namespace Task_1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new HotelViewModel();
        }

        private void ExitClick(object sender, RoutedEventArgs e) => Close();
        private void AboutClick(object sender, RoutedEventArgs e) =>
            MessageBox.Show("Hotel Booking System v1.0\n© 2026\n\nMVVM Pattern", "О программе");
    }
}