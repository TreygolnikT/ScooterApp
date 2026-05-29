using ScooterApp.Models;
using ScooterApp.ViewModels;
using ScooterApp.Views;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ScooterApp
{
    public partial class Menu : Window
    {
        private readonly int _currentUserId;

        public Menu(int userId)
        {
            InitializeComponent();
            _currentUserId = userId;
        }

        public async void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            LoadScooters();

            try
            {
                var res = await MenuViewModel.ChooseWeather();
                Uri uri = new Uri(res, UriKind.Relative);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = uri;
                bitmap.EndInit();
                imgWeather.Source = bitmap;
            }
            catch
            {
                // Можно оставить пустым
            }
        }

        private void LoadScooters()
        {
            using (var db = new AppContext())
            {
                dgScooters.ItemsSource = db.Scooters.ToList();
            }
        }

        private void btnRent_Click(object sender, RoutedEventArgs e)
        {
            if (dgScooters.SelectedItem is not Scooter selectedScooter)
            {
                MessageBox.Show(
                    "Сначала выберите самокат из таблицы.",
                    "Самокат не выбран",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (selectedScooter.Charge < 25)
            {
                MessageBox.Show(
                    "Нельзя начать аренду: заряд самоката меньше 25%.",
                    "Недостаточно заряда",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            RentWindow rent = new RentWindow(_currentUserId, selectedScooter.ScooterId);
            rent.Show();
            Close();
        }

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account(_currentUserId);
            account.Show();
            Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void btnAdminPanel_Click(object sender, RoutedEventArgs e)
        {
            AdminPanelWindow adminPanel = new AdminPanelWindow(_currentUserId);
            adminPanel.Show();
            Close();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            spTools.Visibility = Visibility.Visible;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            spTools.Visibility = Visibility.Collapsed;
        }
    }
}