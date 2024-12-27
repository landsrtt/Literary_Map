using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Literary_Map.Services;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows;
using Literary_Map.Views;

namespace Literary_Map.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        private string _email;
        private string _password;
        private DatabaseService _databaseService;
        private Window _currentWindow;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel(DatabaseService databaseService, Window currentWindow)
        {
            _databaseService = databaseService;
            _currentWindow = currentWindow;
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
        }

        private void Login(object obj)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = _databaseService.GetUserByEmail(Email);

            if (user != null)
            {
                var mainWindow = new MainWindow(user, _databaseService);
                mainWindow.Show();
                _currentWindow.Close();
            }
            else
            {
                MessageBox.Show("Неверный адрес электронной почты или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Register(object obj)
        {

            var registrationWindow = new RegisterView(_databaseService);
            registrationWindow.Show();
            _currentWindow.Close();
        }
    }
}
