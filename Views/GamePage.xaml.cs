using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TicTacToe.Repositories;
using TicTacToe.Services;
using TicTacToe.ViewModels;
using TicTacToe.Views.Utils;

namespace TicTacToe.Views
{
    /// <summary>
    /// Логіка взаємодії для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private readonly GameService _gameService;
        private readonly PlayerService _playerService;
        private readonly MainViewModel _mainViewModel;
        private bool _isGameStarted;

        /// <summary>
        /// Створює новий екземпляр GamePage.
        /// </summary>
        /// <param name="mainViewModel">Головна ViewModel.</param>
        public GamePage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            var playerRepository = new PlayerRepository(connectionString);
            var gameRepository = new GameRepository(connectionString);
            _gameService = new GameService(gameRepository, playerRepository);
            _playerService = new PlayerService(playerRepository);
            _mainViewModel = mainViewModel;
            _isGameStarted = false;

            UpdatePlayerInfo();
        }

        /// <summary>
        /// Обробник події для кнопки "Почати гру".
        /// Перевіряє, чи вибрано рівень бота перед початком гри.
        /// </summary>
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (BotLevelComboBox.SelectedItem == null)
            {
                MessageBox.Show("Будь ласка, оберіть складність бота перед початком гри.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var selectedLevel = ((ComboBoxItem)BotLevelComboBox.SelectedItem).Content.ToString();
            _gameService.StartNewGame(selectedLevel);
            UpdateBoard();
            UpdatePlayerInfo();
            BotLevelComboBox.IsEnabled = false;
            HintCheckBox.IsEnabled = false;
            _isGameStarted = true;
        }

        /// <summary>
        /// Обробник події для кліку по клітинці гри.
        /// Перевіряє, чи почата гра перед дозволом ходу.
        /// </summary>
        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            if (!_isGameStarted)
            {
                MessageBox.Show("Будь ласка, почніть гру перед тим, як робити ходи.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var button = sender as Button;
            var cellName = button.Name;
            var row = int.Parse(cellName[4].ToString());
            var col = int.Parse(cellName[5].ToString());

            if (_gameService.MakeMove(row, col))
            {
                var winner = _gameService.GetCurrentPlayer() == 'X' ? "Гравець" : "Комп'ютер";
                MessageBox.Show($"Переможець: {winner}!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateScores();
                _isGameStarted = false;
            }
            else
            {
                UpdateBoard();
                UpdatePlayerInfo();
                if (HintCheckBox.IsChecked == true)
                {
                    ShowHint();
                }
            }
        }

        /// <summary>
        /// Показує підказку для ходу, якщо включена відповідна опція.
        /// </summary>
        private void ShowHint()
        {
            var (row, col) = _gameService.GetHint();
            var hintButton = (Button)FindName($"Cell{row}{col}");
            if (hintButton != null)
            {
                hintButton.Background = Brushes.LightGray;
            }
        }

        /// <summary>
        /// Оновлює ігрове поле відповідно до поточного стану гри.
        /// </summary>
        private void UpdateBoard()
        {
            var board = _gameService.GetBoard();
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    var cellName = $"Cell{row}{col}";
                    var button = (Button)FindName(cellName);
                    var cellContent = board[row, col];

                    button.Content = cellContent == '\0' ? string.Empty : cellContent.ToString();

                    if (cellContent == 'X')
                    {
                        button.Foreground = Brushes.Blue; // Синій колір для хрестика
                    }
                    else if (cellContent == 'O')
                    {
                        button.Foreground = Brushes.Red; // Червоний колір для нолика
                    }
                    else
                    {
                        button.Foreground = Brushes.Black; // Колір за замовчуванням
                    }

                    button.Background = Brushes.White; // Скидаємо колір фону
                }
            }
        }

        /// <summary>
        /// Оновлює результати гравця після завершення гри.
        /// </summary>
        private void UpdateScores()
        {
            var playerId = MainWindow.GetLoggedInPlayerId();
            var player = _playerService.GetPlayerById(playerId);
            PlayerScoreTextBlock.Text = player.Wins.ToString();
            ComputerScoreTextBlock.Text = player.Losses.ToString();
            BotLevelComboBox.IsEnabled = true;
            HintCheckBox.IsEnabled = true;
        }

        /// <summary>
        /// Оновлює інформацію про гравця.
        /// </summary>
        private void UpdatePlayerInfo()
        {
            var playerId = MainWindow.GetLoggedInPlayerId();
            var player = _playerService.GetPlayerById(playerId);
            CurrentPlayerTextBlock.Text = player.Username;
            PlayerScoreTextBlock.Text = player.Wins.ToString();
            ComputerScoreTextBlock.Text = player.Losses.ToString();
        }

        /// <summary>
        /// Перехід на сторінку профілю гравця.
        /// </summary>
        private void GoToProfileButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.NavigateTo(new ProfilePage(_mainViewModel));
        }

        /// <summary>
        /// Перехід на сторінку рейтингу гравців.
        /// </summary>
        private void RatingButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.NavigateTo(new RatingPage(_mainViewModel));
        }

        /// <summary>
        /// Зміна мови додатку.
        /// </summary>
        private void LanguagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxUtils.HandleSelectionChanged(sender, e, "/Views/Localization/");
        }
    }
}