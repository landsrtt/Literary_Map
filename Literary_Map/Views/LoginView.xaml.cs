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
using Literary_Map.Services;
using Literary_Map.ViewModel;

namespace Literary_Map.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginViewModel ViewModel { get; set; }
        public LoginView(DatabaseService databaseService)
        {
            InitializeComponent();
            ViewModel = new LoginViewModel(databaseService, this);
            DataContext = ViewModel;
        
        }
    }
}

        //private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        ////{
        ////    if (DataContext is LoginViewModel viewModel)
        ////    {
        ////        viewModel.Password = ((PasswordBox)sender).Password;
        ////    }
//        ////}
//        private void PasswordBox_PasswordChanged(object sender,         //{
//System.Windows.RoutedEventArgs e)
//        //    var passwordBox = sender as PasswordBox;
//        //    var viewModel = DataContext as LoginViewModel;
//        //    if (viewModel != null)
//        //    {
//        //        viewModel.Password = passwordBox.Password;
//        //    }
        //}
 
