using ScooterApp.Models;
using ScooterApp.ViewModels;
using System.Linq;
using System.Windows;

namespace ScooterApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel.SetGreeting(txtGreeting);

            DbSeeder.Seed();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) //ВРЕМЕННЫЙ ФЕЙКОВЫЙ ВАРИАНТ ЗАХОДА В ГЛАВНОЕ ОКНО!!!
        {
            using (var db = new AppContext())
            {
                var user = db.Users.FirstOrDefault();

                /*if (user == null)
                {
                    MessageBox.Show("В базе нет пользователей.");
                    return;
                }*/

                Menu menu = new Menu(1);//user.UserId);
                menu.Show();
                Close();
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}