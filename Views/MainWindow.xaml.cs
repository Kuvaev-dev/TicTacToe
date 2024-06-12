using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicTacToe.Repositories;
using TicTacToe.Services;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameService _gameService;
        private readonly PlayerService _playerService;
        private static int _loggedInPlayerId;

        public MainWindow()
        {
            InitializeComponent();
            var connectionString = "your_connection_string_here";
            var playerRepository = new PlayerRepository(connectionString);
            var gameRepository = new GameRepository(connectionString);
            _gameService = new GameService(gameRepository, playerRepository);
            _playerService = new PlayerService(playerRepository);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = UsernameTextBox.Text;
                var password = PasswordBox.Password;
                _playerService.RegisterPlayer(username, password);
                MessageBox.Show("Registration successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = LoginUsernameTextBox.Text;
                var password = LoginPasswordBox.Password;
                var player = _playerService.Login(username, password);
                _loggedInPlayerId = player.Id; // Установка текущего залогиненного пользователя
                MessageBox.Show($"Welcome back, {player.Username}!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void UpdateProfileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var playerId = GetLoggedInPlayerId(); // Получение текущего залогиненного пользователя
                var newUsername = NewUsernameTextBox.Text;
                var newPassword = NewPasswordBox.Password;
                _playerService.UpdateProfile(playerId, newUsername, newPassword);
                MessageBox.Show("Profile updated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = DeleteUsernameTextBox.Text;
                var player = _playerService.GetPlayerByUsername(username); // Ensure this method is available in the service
                if (player != null)
                {
                    _playerService.LogicalDeletePlayer(player.Id);
                    MessageBox.Show("Account deleted successfully!");
                }
                else
                {
                    MessageBox.Show("Player not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private int GetLoggedInPlayerId()
        {
            return _loggedInPlayerId;
        }
    }
}
