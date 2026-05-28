using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScooterApp
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnRent_Click(object sender, RoutedEventArgs e)
        {
            RentWindow rent = new RentWindow();
            rent.Show();
            Close();
        }

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account();
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
            AdminPanelWindow adminPanel = new AdminPanelWindow();
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
