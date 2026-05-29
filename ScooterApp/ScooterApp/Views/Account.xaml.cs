using System.Windows;

namespace ScooterApp
{
    public partial class Account : Window
    {
        private readonly int _currentUserId;

        public Account(int userId)
        {
            InitializeComponent();
            _currentUserId = userId;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(_currentUserId);
            menu.Show();
            Close();
        }
    }
}