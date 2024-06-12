using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using TicTacToe.Repositories;
using TicTacToe.Services;

namespace TicTacToe.Views
{
    public partial class RatingPage : Page
    {
        private readonly PlayerService _playerService;

        public RatingPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            var playerRepository = new PlayerRepository(connectionString);
            _playerService = new PlayerService(playerRepository);
            Loaded += RatingPage_Loaded;
        }

        private void RatingPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var topPlayers = _playerService.GetTopPlayers(5); // Получение топ-5 игроков
                RatingDataGrid.ItemsSource = topPlayers; // Привязка к элементу управления для отображения рейтинга
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
