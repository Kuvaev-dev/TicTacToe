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
        /// Создает новый экземпляр LoginPage.
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

        /// <summary>
        /// Обработчик нажатия кнопки входа в систему.
        /// </summary>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = UsernameTextBox.Text;
                var password = PasswordB.Password;
                var player = _playerService.Login(username, password);
                MainWindow.SetLoggedInPlayerId(player.Id); // Установка текущего залогиненного пользователя
                _mainViewModel.NavigateTo(new GamePage(_mainViewModel));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки перехода на страницу регистрации.
        /// </summary>
        private void GoToRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.NavigateTo(new RegisterPage(_mainViewModel));
        }
    }
}
