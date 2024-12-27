using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Literary_Map.Models;
using Literary_Map.Services;
using Literary_Map.ViewModel;
using System.Windows.Input;
using System.Windows;

namespace Literary_Map.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private User _currentUser;
        private List<Book> _favoriteBooks;
        private Book _selectedFavoriteBook;
        private DatabaseService _databaseService;
        private Window _currentWindow;
        private Book _selectedDetailsBook;

        public ICommand BackToMainCommand { get; }
     
        public User CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(); }
        }
        public List<Book> FavoriteBooks
        {
            get { return _favoriteBooks; }
            set { _favoriteBooks = value; OnPropertyChanged(); }
        }
        public Book SelectedFavoriteBook
        {
            get { return _selectedFavoriteBook; }
            set { _selectedFavoriteBook = value; OnPropertyChanged(); }
        }

        public ICommand RemoveFromFavoritesCommand { get; }

        public ProfileViewModel(User user, DatabaseService databaseService, Window currentWindow)
        {
            _currentUser = user;
            _databaseService = databaseService;
            _currentWindow = currentWindow;

            FavoriteBooks = _databaseService.GetFavoriteBooks(_currentUser.Id);


            RemoveFromFavoritesCommand = new RelayCommand(RemoveFromFavorites);
            BackToMainCommand = new RelayCommand(BackToMain);
        }

        private void RemoveFromFavorites(object obj)
        {
            if (SelectedFavoriteBook != null)
            {
                _databaseService.RemoveBookFromFavorites(_currentUser.Id, SelectedFavoriteBook.Id);
                FavoriteBooks = _databaseService.GetFavoriteBooks(_currentUser.Id);
                MessageBox.Show("Book removed from favorites", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }

        private void BackToMain(object obj)
        {
            // Закрываем текущее окно (ProfileView)
            _currentWindow.Close();

            // Открываем главное окно (MainWindow)
            var mainWindow = new MainWindow(_currentUser, _databaseService);
            mainWindow.Show();
        }


    }
}