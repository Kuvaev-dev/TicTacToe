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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private readonly PlayerService _playerService;
        private readonly MainViewModel _mainViewModel;

        /// <summary>
        /// Создает новый экземпляр ProfilePage.
        /// </summary>
        /// <param name="mainViewModel">Главная ViewModel.</param>
        public ProfilePage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            var playerRepository = new PlayerRepository(connectionString);
            _playerService = new PlayerService(playerRepository);
            _mainViewModel = mainViewModel;
            LoadPlayerData();
        }

        /// <summary>
        /// Загружает данные игрока и отображает их на странице профиля.
        /// </summary>
        private void LoadPlayerData()
        {
            try
            {
                var playerId = MainWindow.GetLoggedInPlayerId();
                var player = _playerService.GetPlayerById(playerId);

                PlayerWinsTextBlock.Text = player.Wins.ToString();
                PlayerLossesTextBlock.Text = player.Losses.ToString();
                PlayerDrawsTextBlock.Text = player.Draws.ToString();
                NewUsernameTextBox.Text = player.Username.ToString();
                NewPasswordBox.Password = player.Password.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження даних гравця: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки обновления профиля.
        /// </summary>
        private void UpdateProfileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var playerId = MainWindow.GetLoggedInPlayerId();
                var newUsername = NewUsernameTextBox.Text;
                var newPassword = NewPasswordBox.Password;
                _playerService.UpdateProfile(playerId, newUsername, newPassword);
                MessageBox.Show("Профіль успішно оновлено!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки удаления аккаунта.
        /// </summary>
        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var playerId = MainWindow.GetLoggedInPlayerId();
                _playerService.LogicalDeletePlayer(playerId);
                MessageBox.Show("Акаунт успішно видалено!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Error);
                MainWindow.SetLoggedInPlayerId(0);
                _mainViewModel.NavigateTo(new LoginPage(_mainViewModel));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки выхода из системы.
        /// </summary>
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var navigationService = mainWindow.MainFrame.NavigationService;
            while (navigationService.RemoveBackEntry() != null) { }

            MainWindow.SetLoggedInPlayerId(0);
            _mainViewModel.NavigateTo(new LoginPage(_mainViewModel));
        }
    }
}
