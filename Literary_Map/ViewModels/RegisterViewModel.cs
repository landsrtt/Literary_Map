using System.Windows;
using System.Windows.Input;
using Literary_Map.Models;
using Literary_Map.Services;
using Literary_Map.Views;

namespace Literary_Map.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private string _name;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private DatabaseService _databaseService;
        private Window _currentWindow;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        
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
         public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChanged(); }
        }


        public ICommand RegisterCommand { get; }

        public RegisterViewModel(DatabaseService databaseService, Window currentWindow)
        {
            _databaseService = databaseService;
            _currentWindow = currentWindow;
            RegisterCommand = new RelayCommand(Register);
        }

        private void Register(object obj)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBox.Show("Please fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = new User
            {
                Username = Name,
                Email = Email,
                Password = Password
            };

            if (_databaseService.RegisterUser(user))
            {
                MessageBox.Show("Registration successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                var loginWindow = new LoginView(_databaseService);
                loginWindow.Show();
                _currentWindow.Close();
            }
            else
            {
                MessageBox.Show("User with this email already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}