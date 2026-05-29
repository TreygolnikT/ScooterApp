using System.Configuration;
using System.Data;
using System.Windows;
using ScooterApp.Models;
namespace ScooterApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DbSeeder.Seed();
            base.OnStartup(e);
        }
    }

}
