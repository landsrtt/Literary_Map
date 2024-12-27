using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GMap.NET;
using Literary_Map.Models;
using Literary_Map.Services;
using Literary_Map.Views;

namespace Literary_Map.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private List<Book> _books;
        private Book _selectedBook;
        private string _searchText;
        private List<Book> _searchResults;
        private DatabaseService DatabaseService;
        private User _currentUser;
        private PointLatLng _mapCenter = new PointLatLng(0, 0);
        private bool _isBookDetailsVisible;
        private Book _selectedDetailsBook;
        private bool _isUserInFavorites;
        private Window _currentWindow;

        public List<Book> Books
        {
            get { return _books; }
            set { _books = value; OnPropertyChanged(); }
        }

        public Book SelectedBook
        {
            get { return _selectedBook; }
            set { _selectedBook = value; OnPropertyChanged(); }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                //SearchBooks();
                OnPropertyChanged();
            }
        }

        public List<Book> SearchResults
        {
            get { return _searchResults; }
            set { _searchResults = value; OnPropertyChanged(); }
        }
        public PointLatLng MapCenter
        {
            get { return _mapCenter; }
            set { _mapCenter = value; OnPropertyChanged(); }
        }

        public bool IsBookDetailsVisible
        {
            get { return _isBookDetailsVisible; }
            set { _isBookDetailsVisible = value; OnPropertyChanged(); }
        }
        public Book SelectedDetailsBook
        {
            get { return _selectedDetailsBook; }
            set { _selectedDetailsBook = value; OnPropertyChanged(); }
        }
        public bool IsUserInFavorites
        {
            get { return _isUserInFavorites; }
            set { _isUserInFavorites = value; OnPropertyChanged(); }
        }
        public ICommand SearchCommand { get; }
        public ICommand BookSelectionChangedCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand AddToFavoritesCommand { get; }
        public ICommand CloseBookDetailsCommand { get; }
        public ICommand SelectionChangedCommand { get; }


        public MainViewModel(User user, DatabaseService databaseService, Window currentWindow)
        {
            SearchCommand = new RelayCommand(SearchBooks);
            AddToFavoritesCommand = new RelayCommand(AddToFavorites);
            DatabaseService = databaseService;
            _currentUser = user;
            _currentWindow = currentWindow;
            Books = new List<Book>();
            Books = DatabaseService.GetAllBooks();
            BookSelectionChangedCommand = new RelayCommand(BookSelectionChanged);
            ProfileCommand = new RelayCommand(OpenProfile);
            CloseBookDetailsCommand = new RelayCommand(CloseBookDetails);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);

        }

        private void SelectionChanged(object obj)
        {
            if (obj is System.Windows.Controls.ComboBox comboBox)
            {
                if (comboBox.SelectedItem is Book selectedItem)
                {
                    SelectedBook = selectedItem;
                    BookSelectionChanged(obj);
                }
            }
        }
        private bool _isBookPopupOpen;
        public bool IsBookPopupOpen
        {
            get => _isBookPopupOpen;
            set
            {
                _isBookPopupOpen = value;
                OnPropertyChanged();
            }
        }
        private void CloseBookDetails(object obj)
        {
            IsBookDetailsVisible = false;
            MapCenter = new PointLatLng(0, 0);
            SelectedDetailsBook = null; 
            IsBookPopupOpen = false; 

        }



        public void AddToFavorites(object obj)
        {
            if (SelectedDetailsBook != null)
            {
                if (!DatabaseService.IsBookInFavorites(_currentUser.Id, SelectedDetailsBook.Id))
                {
                    DatabaseService.AddBookToFavorites(_currentUser.Id, SelectedDetailsBook.Id);
                    IsUserInFavorites = true;
                    MessageBox.Show("Книга добавлена в избранное", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Книга уже в избранном списке", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        public void SearchBooks(object obj = null)
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                SearchResults = Books.Where(b => b.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
                if (SearchResults.Count > 0)
                {
                    SelectedBook = SearchResults.First();
                    BookSelectionChanged(null);
                }
                else
                {
                    MessageBox.Show("Книги не найдены", "Поиск", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


        private void BookSelectionChanged(object obj)
        {
            if (SelectedBook != null)
            {
                MapCenter = new PointLatLng(SelectedBook.Latitude, SelectedBook.Longitude);
                IsBookDetailsVisible = true;
                SelectedDetailsBook = SelectedBook;
                IsUserInFavorites = DatabaseService.IsBookInFavorites(_currentUser.Id, SelectedDetailsBook.Id);
                IsBookPopupOpen = true; 
            }
        }




        private void OpenProfile(object obj)
        {
            var profileWindow = new ProfileView(_currentUser, DatabaseService);
            profileWindow.Show();
            _currentWindow.Close();
        }

    }
}
