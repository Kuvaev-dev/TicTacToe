﻿using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using TicTacToe.Repositories;
using TicTacToe.Services;
using TicTacToe.ViewModels;

namespace TicTacToe.Views
{
    /// <summary>
    /// Логіка взаємодії для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private readonly PlayerService _playerService;
        private readonly MainViewModel _mainViewModel;

        /// <summary>
        /// Створює новий екземпляр сторінки реєстрації.
        /// </summary>
        /// <param name="mainViewModel">Модель представлення головного вікна.</param>
        public RegisterPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            var playerRepository = new PlayerRepository(connectionString);
            _playerService = new PlayerService(playerRepository);
            _mainViewModel = mainViewModel;
        }

        /// <summary>
        /// Обробляє натискання кнопки реєстрації.
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
        /// Обробляє натискання кнопки переходу на сторінку входу.
        /// </summary>
        private void GoToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.NavigateTo(new LoginPage(_mainViewModel));
        }
    }
}