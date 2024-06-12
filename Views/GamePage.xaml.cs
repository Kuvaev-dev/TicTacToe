using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using TicTacToe.Repositories;
using TicTacToe.Services;

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

        /// <summary>
        /// Создает новый экземпляр GamePage.
        /// </summary>
        /// <param name="mainViewModel">Главная ViewModel.</param>
        public GamePage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            var connectionString = "your_connection_string_here";
            var playerRepository = new PlayerRepository(connectionString);
            var gameRepository = new GameRepository(connectionString);
            _gameService = new GameService(gameRepository, playerRepository);
            _playerService = new PlayerService(playerRepository);
            _mainViewModel = mainViewModel;
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedLevel = ((ComboBoxItem)BotLevelComboBox.SelectedItem).Content.ToString();
            _gameService.StartNewGame(selectedLevel);
            UpdateBoard();
            CurrentPlayerTextBlock.Text = $"Текущий игрок: {_gameService.GetCurrentPlayer()}";
            BotLevelComboBox.IsEnabled = false;
            HintCheckBox.IsEnabled = false;
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var cellName = button.Name;
            var row = int.Parse(cellName[4].ToString());
            var col = int.Parse(cellName[5].ToString());

            if (_gameService.MakeMove(row, col))
            {
                MessageBox.Show($"Победитель: {_gameService.GetCurrentPlayer()}!");
                UpdateScores();
                BotLevelComboBox.IsEnabled = true;
                HintCheckBox.IsEnabled = true;
            }
            else
            {
                UpdateBoard();
                CurrentPlayerTextBlock.Text = $"Текущий игрок: {_gameService.GetCurrentPlayer()}";
                if (HintCheckBox.IsChecked == true)
                {
                    ShowHint();
                }
            }
        }

        private void ShowHint()
        {
            var hint = _gameService.GetHint();
            var hintButton = (Button)FindName($"Cell{hint.row}{hint.col}");
            hintButton.Background = Brushes.LightGray;
        }

        private void UpdateBoard()
        {
            var board = _gameService.GetBoard();
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    var cellName = $"Cell{row}{col}";
                    var button = (Button)FindName(cellName);
                    button.Content = board[row, col] == '\0' ? string.Empty : board[row, col].ToString();
                    button.Background = Brushes.White; // Reset background color
                }
            }
        }

        private void UpdateScores()
        {
            //var playerId = MainWindow.GetLoggedInPlayerId();
            //var player = _playerService.GetPlayerById(playerId);
            //PlayerScoreTextBlock.Text = player.Wins.ToString();

            //var botPlayer = _playerService.GetPlayerById(2); // Example bot player ID
            //ComputerScoreTextBlock.Text = botPlayer.Wins.ToString();
        }

        private void GoToProfileButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.NavigateTo(new ProfilePage(_mainViewModel));
        }
    }
}
