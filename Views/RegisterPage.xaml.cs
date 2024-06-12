using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TicTacToe.Repositories;
using TicTacToe.Services;

namespace TicTacToe.Views
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private readonly PlayerService _playerService;

        public RegisterPage()
        {
            InitializeComponent();
            var connectionString = "your_connection_string_here";
            var playerRepository = new PlayerRepository(connectionString);
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
                NavigationService.Navigate(new LoginPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void GoToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}
