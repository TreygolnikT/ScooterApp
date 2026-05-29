using ScooterApp.Models;
using ScooterApp.ViewModels;
using System;
using System.Windows;

namespace ScooterApp.Views
{
    public partial class RentWindow : Window
    {
        private readonly RentViewModel _viewModel;
        private readonly int _userId;

        public RentWindow(int userId, int scooterId)
        {
            InitializeComponent();

            _userId = userId;
            _viewModel = new RentViewModel(userId, scooterId);
            DataContext = _viewModel;

            Loaded += RentWindow_Loaded;
            Closing += RentWindow_Closing;
        }

        private void RentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.StartRide();
        }

        private void RentWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _viewModel.StopTimer();
        }

        private void btnStopRide_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Кнопка \"СТОП\" пока не реализована.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Вы уверены, что хотите завершить поездку?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            string error = _viewModel.TryFinishRide();

            if (error != null)
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Спасибо за поездку! Оплата прошла успешно.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

            Menu menuWindow = new Menu(_userId);
            menuWindow.Show();
            Close();
        }
    }
}