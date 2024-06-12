using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using TicTacToe.Repositories;
using TicTacToe.Services;

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
        /// Создает новый экземпляр страницы профиля.
        /// </summary>
        /// <param name="mainViewModel">Модель представления главного окна.</param>
        public ProfilePage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            var playerRepository = new PlayerRepository(connectionString);
            _playerService = new PlayerService(playerRepository);
            _mainViewModel = mainViewModel;
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки обновления профиля.
        /// </summary>
        private void UpdateProfileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var playerId = MainWindow.GetLoggedInPlayerId();
                var newUsername = NewUsernameTextBox.Text;
                var newPassword = NewPasswordBox.Password;
                _playerService.UpdateProfile(playerId, newUsername, newPassword);
                MessageBox.Show("Профиль успешно обновлен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки удаления аккаунта.
        /// </summary>
        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var playerId = MainWindow.GetLoggedInPlayerId();
                _playerService.LogicalDeletePlayer(playerId);
                MessageBox.Show("Аккаунт успешно удален!");
                MainWindow.SetLoggedInPlayerId(0);
                _mainViewModel.NavigateTo(new LoginPage(_mainViewModel));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки выхода из аккаунта.
        /// </summary>
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetLoggedInPlayerId(0);
            _mainViewModel.NavigateTo(new LoginPage(_mainViewModel));
        }
    }
}