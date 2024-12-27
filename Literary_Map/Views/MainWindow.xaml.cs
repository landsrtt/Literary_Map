using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using Literary_Map.Models;
using Literary_Map.Services;
using Literary_Map.ViewModel;


namespace Literary_Map
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        public MainWindow(User user, DatabaseService databaseService)
        {
            InitializeComponent();


            _viewModel = new MainViewModel(user, databaseService, this);
            DataContext = _viewModel;


            // Инициализация карты
            MapControl.MapProvider = GMapProviders.OpenStreetMap; 
            MapControl.Position = new PointLatLng(0, 0); 
            MapControl.MinZoom = 1;
            MapControl.MaxZoom = 18;
            MapControl.Zoom = 2;

            // Добавление меток для книг
            foreach (var book in _viewModel.Books)
            {
                var marker = new GMapMarker(new PointLatLng(book.Latitude, book.Longitude))
                {
                    Shape = new Button
                    {
                        Content = book.Title,
                        Width = 50,
                        Height = 30,
                        ToolTip = $"Автор: {book.Author}\nЖанр: {book.Genre}",
                        Tag = book
                    }
                };
                ((Button)marker.Shape).Click += OnMarkerClick;
                MapControl.Markers.Add(marker);
            }
        }
        private void OnMarkerClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var book = button?.Tag as Book;
            if (book != null)
            {
                _viewModel.SelectedDetailsBook = book; 
                _viewModel.IsBookDetailsVisible = true; 
                BookPopup.IsOpen = true; 
            }
        }

        private void OnAddToFavoritesClick(object sender, RoutedEventArgs e)
        {
            var book = (Book)((Button)sender).Tag;
            _viewModel.AddToFavorites(book);
            MessageBox.Show("Книга добавлена в избранное", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnClosePopupClick(object sender, RoutedEventArgs e)
        {
            BookPopup.IsOpen = false;
        }



        private void OnSearchClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SearchBox.Text))
            {
                _viewModel.SearchText = SearchBox.Text; 
                _viewModel.SearchBooks(); 
            }
        }



        private void OnProfileClick(object sender, RoutedEventArgs e)
        {
            _viewModel.ProfileCommand.Execute(null); 
        }

        private void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
           
            GMaps.Instance.Mode = AccessMode.ServerAndCache; 
        }

        private void MapControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
            var point = MapControl.FromLocalToLatLng((int)e.GetPosition(MapControl).X, (int)e.GetPosition(MapControl).Y);
            MessageBox.Show($"Вы выбрали: {point.Lat}, {point.Lng}");
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}