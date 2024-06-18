using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TicTacToe.Repositories;
using TicTacToe.Services;
using TicTacToe.ViewModels;

namespace TicTacToe.Views
{
    /// <summary>
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private readonly GameService _gameService;
        private readonly PlayerService _playerService;
        private readonly MainViewModel _mainViewModel;
        private bool _isGameStarted;

        /// <summary>
        /// Создает новый экземпляр GamePage.
        /// </summary>
        /// <param name="mainViewModel">Главная ViewModel.</param>
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
        /// Обработчик события для кнопки "Начать игру".
        /// Проверяет, выбран ли уровень бота перед началом игры.
        /// </summary>
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (BotLevelComboBox.SelectedItem == null)
            {
                MessageBox.Show("Будь-ласка, оберіть складність бота перед початком гри.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
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
        /// Обработчик события для клика по клетке игры.
        /// Проверяет, начата ли игра перед тем, как позволить ход.
        /// </summary>
        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            if (!_isGameStarted)
            {
                MessageBox.Show("Будь-ласка, почніть гру перед тим, як робити ходи.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var button = sender as Button;
            var cellName = button.Name;
            var row = int.Parse(cellName[4].ToString());
            var col = int.Parse(cellName[5].ToString());

            if (_gameService.MakeMove(row, col))
            {
                MessageBox.Show($"Переможець: {_gameService.GetCurrentPlayer()}!");
                UpdateScores();
                BotLevelComboBox.IsEnabled = true;
                HintCheckBox.IsEnabled = true;
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
        /// Показывает подсказку для хода, если включена соответствующая опция.
        /// </summary>
        private void ShowHint()
        {
            var hint = _gameService.GetHint();
            var hintButton = (Button)FindName($"Cell{hint.row}{hint.col}");
            if (hintButton != null)
            {
                hintButton.Background = Brushes.LightGray;
            }
        }

        /// <summary>
        /// Обновляет игровое поле в соответствии с текущим состоянием игры.
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
                        button.Foreground = Brushes.Blue; // Синий цвет для крестика
                    }
                    else if (cellContent == 'O')
                    {
                        button.Foreground = Brushes.Red; // Красный цвет для нолика
                    }
                    else
                    {
                        button.Foreground = Brushes.Black; // Цвет по умолчанию
                    }

                    button.Background = Brushes.White; // Сбрасываем цвет фона
                }
            }
        }

        /// <summary>
        /// Обновляет результаты игрока после завершения игры.
        /// </summary>
        private void UpdateScores()
        {
            var playerId = MainWindow.GetLoggedInPlayerId();
            var player = _playerService.GetPlayerById(playerId);
            PlayerScoreTextBlock.Text = player.Wins.ToString();
            ComputerScoreTextBlock.Text = player.Losses.ToString();
        }

        /// <summary>
        /// Обновляет информацию об игроке.
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
        /// Переход на страницу профиля игрока.
        /// </summary>
        private void GoToProfileButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.NavigateTo(new ProfilePage(_mainViewModel));
        }

        /// <summary>
        /// Переход на страницу рейтинга игроков.
        /// </summary>
        private void RatingButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.NavigateTo(new RatingPage(_mainViewModel));
        }
    }
}
