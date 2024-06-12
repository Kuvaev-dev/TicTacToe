using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
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

        public ProfilePage()
        {
            InitializeComponent();
            var connectionString = "your_connection_string_here";
            var playerRepository = new PlayerRepository(connectionString);
            _playerService = new PlayerService(playerRepository);
        }

        private void UpdateProfileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var playerId = MainWindow.GetLoggedInPlayerId();
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
                var playerId = MainWindow.GetLoggedInPlayerId();
                _playerService.LogicalDeletePlayer(playerId);
                MessageBox.Show("Account deleted successfully!");
                MainWindow.SetLoggedInPlayerId(0);
                NavigationService.Navigate(new LoginPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetLoggedInPlayerId(0);
            NavigationService.Navigate(new LoginPage());
        }
    }
}
