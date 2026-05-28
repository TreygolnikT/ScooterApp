using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterApp.ViewModels
{
    // ViewModel главного окна (регистрации)
    static class MainViewModel
    {
        // Метод для установки приветствия в зависимости от времени суток
        public static void SetGreeting(System.Windows.Controls.TextBlock txtGreeting)
        {
            if (DateTime.Now.Hour < 4)
            {
                txtGreeting.Text = "Доброй ночи!";
            }
            else if (DateTime.Now.Hour < 12)
            {
                txtGreeting.Text = "Доброе утро!";
            }
            else if (DateTime.Now.Hour < 16)
            {
                txtGreeting.Text = "Добрый день!";
            }
            else
            {
                txtGreeting.Text = "Добрый вечер!";
            }
        }
    }
}
