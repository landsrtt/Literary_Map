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
    /// Логика взаимодействия для RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        public RegisterViewModel ViewModel { get; set; }
        public RegisterView(DatabaseService databaseService)
        {
            InitializeComponent();
            ViewModel = new RegisterViewModel(databaseService, this);
            DataContext = ViewModel;
        }
        //private void OnRegisterClick(object sender, RoutedEventArgs e)
        //{
        //    _viewModel.Username = UsernameBox.Text;
        //    _viewModel.Password = PasswordBox.Password;
        //    _viewModel.Email = EmailBox.Text;

        //    if (_viewModel.Register(null)) // Теперь метод возвращает bool
        //    {
        //        var loginView = new LoginView();
        //        loginView.Show();
        //        this.Close();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Registration failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    if (DataContext is RegisterViewModel viewModel)
        //    {
        //        viewModel.Password = ((PasswordBox)sender).Password;
        //    }
        //}
        private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            var viewModel = DataContext as RegisterViewModel; 
            if (viewModel != null)
            {
                viewModel.Password = passwordBox.Password;
            }
        }
    }
        
}
