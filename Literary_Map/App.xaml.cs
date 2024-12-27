using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Literary_Map.Services;
using Literary_Map.Views;

namespace Literary_Map
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var databaseService = new DatabaseService();
            var LoginView = new LoginView(databaseService);
            LoginView.Show();
        }
    }
}
