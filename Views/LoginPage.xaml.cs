using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TicTacToe.Repositories;
using TicTacToe.Services;

namespace TicTacToe.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly PlayerService _playerService;

        public LoginPage()
        {
            InitializeComponent();
            var connectionString = "your_connection_string_here";
            var playerRepository = new PlayerRepository(connectionString);
            _playerService = new PlayerService(playerRepository);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = UsernameTextBox.Text;
                var password = PasswordBox.Password;
                var player = _playerService.Login(username, password);
                MainWindow.SetLoggedInPlayerId(player.Id); // Установка текущего залогиненного пользователя
                NavigationService.Navigate(new GamePage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void GoToRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}
