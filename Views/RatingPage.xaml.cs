using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using TicTacToe.Repositories;
using TicTacToe.Services;
using TicTacToe.ViewModels;

namespace TicTacToe.Views
{
    /// <summary>
    /// Логіка взаємодії для RatingPage.xaml
    /// </summary>
    public partial class RatingPage : Page
    {
        private readonly PlayerService _playerService;
        private readonly MainViewModel _mainViewModel;

        public RatingPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            var playerRepository = new PlayerRepository(connectionString);
            _playerService = new PlayerService(playerRepository);
            _mainViewModel = mainViewModel;
            Loaded += RatingPage_Loaded;
        }

        private void RatingPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var topPlayers = _playerService.GetTopPlayers(5); // Отримання топ-5 гравців
                RatingDataGrid.ItemsSource = topPlayers; // Прив'язка до елементу управління для відображення рейтингу
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
