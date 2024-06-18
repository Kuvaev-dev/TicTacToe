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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private readonly PlayerService _playerService;
        private readonly MainViewModel _mainViewModel;

        /// <summary>
        /// Создает новый экземпляр страницы регистрации.
        /// </summary>
        /// <param name="mainViewModel">Модель представления главного окна.</param>
        public RegisterPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            var playerRepository = new PlayerRepository(connectionString);
            _playerService = new PlayerService(playerRepository);
            _mainViewModel = mainViewModel;
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки регистрации.
        /// </summary>
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = UsernameTextBox.Text;
                var password = PasswordB.Password;
                _playerService.RegisterPlayer(username, password);
                MessageBox.Show("Реєстрація успішна!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                _mainViewModel.NavigateTo(new LoginPage(_mainViewModel));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки перехода к странице входа.
        /// </summary>
        private void GoToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.NavigateTo(new LoginPage(_mainViewModel));
        }
    }
}
