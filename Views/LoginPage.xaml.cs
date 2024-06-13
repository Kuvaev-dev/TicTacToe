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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly PlayerService _playerService;
        private readonly MainViewModel _mainViewModel;

        /// <summary>
        /// Создает новый экземпляр GamePage.
        /// </summary>
        /// <param name="mainViewModel">Главная ViewModel.</param>
        public LoginPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            var playerRepository = new PlayerRepository(connectionString);
            _playerService = new PlayerService(playerRepository);
            _mainViewModel = mainViewModel;
        }

        public LoginPage()
        {
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = UsernameTextBox.Text;
                var password = PasswordBox.Password;
                var player = _playerService.Login(username, password);
                MainWindow.SetLoggedInPlayerId(player.Id); // Установка текущего залогиненного пользователя
                _mainViewModel.NavigateTo(new GamePage(_mainViewModel));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void GoToRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.NavigateTo(new RegisterPage(_mainViewModel));
        }
    }
}
