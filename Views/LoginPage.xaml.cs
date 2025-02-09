﻿using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using TicTacToe.Repositories;
using TicTacToe.Services;
using TicTacToe.ViewModels;
using TicTacToe.Views.Utils;

namespace TicTacToe.Views
{
    /// <summary>
    /// Логіка взаємодії для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly PlayerService _playerService;
        private readonly MainViewModel _mainViewModel;

        /// <summary>
        /// Створює новий екземпляр LoginPage.
        /// </summary>
        /// <param name="mainViewModel">Головна ViewModel.</param>
        public LoginPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            var playerRepository = new PlayerRepository(connectionString);
            _playerService = new PlayerService(playerRepository);
            _mainViewModel = mainViewModel;
            DataContext = Application.Current;
        }

        /// <summary>
        /// Обробник натискання кнопки входу в систему.
        /// </summary>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = UsernameTextBox.Text;
                var password = PasswordB.Password;
                var player = _playerService.Login(username, password);
                MainWindow.SetLoggedInPlayerId(player.Id); // Встановлення ID поточного залогіненого користувача
                var navigationService = NavigationService;
                while (navigationService.RemoveBackEntry() != null) { }
                _mainViewModel.NavigateTo(new GamePage(_mainViewModel));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", (string)Application.Current.FindResource("StringError"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обробник натискання кнопки переходу на сторінку реєстрації.
        /// </summary>
        private void GoToRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.NavigateTo(new RegisterPage(_mainViewModel));
        }

        /// <summary>
        /// Зміна мови додатку.
        /// </summary>
        private void LanguagesComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxUtils.LoadSelectedLanguage(sender, e);
        }

        private void LanguagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxUtils.HandleSelectionChanged(sender, e);
        }
    }
}