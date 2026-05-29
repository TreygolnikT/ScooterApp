using ScooterApp.Models;
using System;
using System.Linq;
using System.Windows;

namespace ScooterApp
{
    public partial class AdminPanelWindow : Window
    {
        private readonly int _currentUserId;

        public AdminPanelWindow(int userId)
        {
            InitializeComponent();
            _currentUserId = userId;

            Loaded += AdminPanelWindow_Loaded;
        }

        private void AdminPanelWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadScooters();
        }

        private void LoadScooters()
        {
            using (var db = new AppContext())
            {
                dgAdminScooters.ItemsSource = db.Scooters.ToList();
            }
        }

        private void btnAddScooter_Click(object sender, RoutedEventArgs e)
        {
            string model = txtModel.Text.Trim();
            string coordsText = txtCoordinates.Text.Trim();

            if (string.IsNullOrWhiteSpace(model) || string.IsNullOrWhiteSpace(coordsText))
            {
                MessageBox.Show(
                    "Введите модель и ID координат.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(coordsText, out int coordsId))
            {
                MessageBox.Show(
                    "Координаты должны быть числом (CoordsId).",
                    "Ошибка формата",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            using (var db = new AppContext())
            {
                var coordExists = db.Coords.Any(c => c.CoordId == coordsId);

                if (!coordExists)
                {
                    MessageBox.Show(
                        "Координаты с таким ID не найдены в базе.",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                Scooter newScooter = new Scooter
                {
                    Model = model,
                    Charge = 100,
                    CoordsId = coordsId
                };

                db.Scooters.Add(newScooter);
                db.SaveChanges();
            }

            txtModel.Clear();
            txtCoordinates.Clear();

            LoadScooters();

            MessageBox.Show(
                "Самокат успешно добавлен.",
                "Успешно",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void btnDeleteScooter_Click(object sender, RoutedEventArgs e)
        {
            if (dgAdminScooters.SelectedItem is not Scooter selectedScooter)
            {
                MessageBox.Show(
                    "Выберите самокат для удаления.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Удалить самокат ID {selectedScooter.ScooterId}?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            using (var db = new AppContext())
            {
                var scooterToDelete = db.Scooters.FirstOrDefault(s => s.ScooterId == selectedScooter.ScooterId);

                if (scooterToDelete == null)
                {
                    MessageBox.Show(
                        "Самокат не найден в базе.",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                db.Scooters.Remove(scooterToDelete);
                db.SaveChanges();
            }

            LoadScooters();

            MessageBox.Show(
                "Самокат удалён.",
                "Успешно",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void btnBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(_currentUserId);
            menu.Show();
            Close();
        }
    }
}