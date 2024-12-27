using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Literary_Map.Models;
using Literary_Map.Services;
using Literary_Map.ViewModels;

namespace Literary_Map.Views
{
    /// <summary>
    /// Логика взаимодействия для ProfileView.xaml
    /// </summary>
    public partial class ProfileView : Window
    {
        public ProfileViewModel ViewModel { get; set; }
        public ProfileView(User user, DatabaseService databaseService)
        {
            InitializeComponent();
            ViewModel = new ProfileViewModel(user, databaseService, this);
            DataContext = ViewModel;
        }
    
    }
}
