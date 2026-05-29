using ScooterApp.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace ScooterApp.ViewModels
{
    internal class RentViewModel : INotifyPropertyChanged
    {
        private readonly AppContext _db;
        private readonly int _userId;
        private readonly int _scooterId;

        private User _currentUser;
        private Scooter _currentScooter;
        private DispatcherTimer _timer;
        private TimeSpan _rideTime;
        private double _pricePerSecond;
        private bool _isRideFinished;

        private string _timerText;
        public string TimerText
        {
            get => _timerText;
            set { _timerText = value; OnPropertyChanged(); }
        }

        private string _moneyText;
        public string MoneyText
        {
            get => _moneyText;
            set { _moneyText = value; OnPropertyChanged(); }
        }

        private string _scooterIdText;
        public string ScooterIdText
        {
            get => _scooterIdText;
            set { _scooterIdText = value; OnPropertyChanged(); }
        }

        private string _chargeText;
        public string ChargeText
        {
            get => _chargeText;
            set { _chargeText = value; OnPropertyChanged(); }
        }

        private string _rideStatus;
        public string RideStatus
        {
            get => _rideStatus;
            set { _rideStatus = value; OnPropertyChanged(); }
        }

        private string _warningText;
        public string WarningText
        {
            get => _warningText;
            set { _warningText = value; OnPropertyChanged(); }
        }

        public double CurrentPrice => Math.Round(_rideTime.TotalSeconds * _pricePerSecond, 2);
        public int CurrentLength => (int)Math.Ceiling(_rideTime.TotalMinutes);

        public RentViewModel(int userId, int scooterId)
        {
            _db = new AppContext();
            _userId = userId;
            _scooterId = scooterId;

            LoadData();
            InitializeDisplay();
        }

        private void LoadData()
        {
            _currentUser = _db.Users.FirstOrDefault(u => u.UserId == _userId);
            _currentScooter = _db.Scooters.FirstOrDefault(s => s.ScooterId == _scooterId);

            if (_currentUser == null || _currentScooter == null)
                throw new InvalidOperationException("Не удалось загрузить пользователя или самокат из базы данных.");

            _pricePerSecond = GetPricePerSecondByModel(_currentScooter.Model);
        }

        private void InitializeDisplay()
        {
            _rideTime = TimeSpan.Zero;
            _isRideFinished = false;

            TimerText = "00:00:00";
            MoneyText = "0.00 ₽";
            ScooterIdText = _currentScooter.ScooterId.ToString();
            ChargeText = $"{_currentScooter.Charge}%";
            RideStatus = "Поездка активна";
            WarningText = string.Empty;
        }

        public void StartRide()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer?.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_isRideFinished)
                return;

            _rideTime = _rideTime.Add(TimeSpan.FromSeconds(1));
            TimerText = _rideTime.ToString(@"hh\:mm\:ss");
            MoneyText = $"{CurrentPrice:0.00} ₽";

            UpdateCharge();

            if (_currentScooter.Charge < 25)
            {
                StopTimer();
                _isRideFinished = true;
                RideStatus = "Поездка остановлена автоматически";
                WarningText = "Заряд самоката ниже 25%. Завершите аренду.";
            }
        }

        private void UpdateCharge()
        {
            if (_rideTime.TotalSeconds % 20 == 0 && _currentScooter.Charge > 0)
            {
                _currentScooter.Charge--;
                ChargeText = $"{_currentScooter.Charge}%";
            }
        }

        private double GetPricePerSecondByModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
                return 0.10;

            string normalized = model.ToLower();

            if (normalized.Contains("max") || normalized.Contains("pro") || normalized.Contains("ultra") || normalized.Contains("premium"))
                return 0.30;

            if (normalized.Contains("lite") || normalized.Contains("econom") || normalized.Contains("mini"))
                return 0.10;

            return 0.20;
        }

        public bool CanFinishRide()
        {
            return !_isRideFinished || _currentScooter.Charge < 25;
        }

        public string TryFinishRide()
        {
            StopTimer();

            double totalPrice = CurrentPrice;

            if (_currentUser.Money < totalPrice)
            {
                RideStatus = "Недостаточно средств";
                WarningText = "Пополните баланс и повторите попытку.";
                return "Недостаточно средств на счёте.";
            }

            _currentUser.Money -= totalPrice;

            var history = new RentalHistory
            {
                Price = totalPrice,
                Length = Math.Max(1, CurrentLength),
                ScooterId = _currentScooter.ScooterId,
                UserId = _currentUser.UserId
            };

            _db.RentalHistories.Add(history);
            _db.SaveChanges();

            _isRideFinished = true;
            RideStatus = "Поездка завершена";
            WarningText = "Спасибо за поездку!";

            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}