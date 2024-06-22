using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TicTacToe.Models;
using TicTacToe.Repositories;

namespace TicTacToe.Services
{
    /// <summary>
    /// Сервіс, що відповідає за управління гравцями.
    /// </summary>
    public class PlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        /// <summary>
        /// Реєструє нового гравця.
        /// </summary>
        /// <param name="username">Ім'я користувача.</param>
        /// <param name="password">Пароль користувача.</param>
        public void RegisterPlayer(string username, string password)
        {
            // Перевірка на порожні рядки
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException((string)Application.Current.FindResource("StringUsernamePasswordEmptyError"));

            // Перевірка на довжину пароля
            if (password.Length < 6)
                throw new ArgumentException((string)Application.Current.FindResource("StringPasswordLengthError"));

            // Перевірка на існування користувача з таким іменем
            var existingPlayer = _playerRepository.GetPlayerByUsername(username);
            if (existingPlayer != null)
                throw new ArgumentException((string)Application.Current.FindResource("StringUsernameExistError"));

            // Створення нового гравця
            var player = new Player
            {
                Username = username,
                Password = password,
                IsDeleted = false,
                GamesPlayed = 0,
                Wins = 0,
                Losses = 0,
                Draws = 0,
                LastLogin = DateTime.Now
            };

            // Додавання гравця до репозиторію
            _playerRepository.AddPlayer(player);
        }

        /// <summary>
        /// Оновлює профіль гравця.
        /// </summary>
        /// <param name="playerId">Ідентифікатор гравця.</param>
        /// <param name="newUsername">Нове ім'я користувача.</param>
        /// <param name="newPassword">Новий пароль користувача.</param>
        public void UpdateProfile(int playerId, string newUsername, string newPassword)
        {
            // Отримання інформації про гравця за ідентифікатором
            var player = _playerRepository.GetPlayerById(playerId);
            if (player == null || player.IsDeleted)
                throw new ArgumentException((string)Application.Current.FindResource("StringUserNotFoundOrIsDeletedError"));

            // Перевірка на порожні рядки
            if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException((string)Application.Current.FindResource("StringUsernamePasswordEmptyError"));

            // Перевірка та оновлення імені користувача
            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                var existingPlayer = _playerRepository.GetPlayerByUsername(newUsername);
                if (existingPlayer != null && existingPlayer.Id != playerId)
                    throw new ArgumentException((string)Application.Current.FindResource("StringUsernameExistError"));

                player.Username = newUsername;
            }

            // Перевірка та оновлення пароля користувача
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                if (newPassword.Length < 6)
                    throw new ArgumentException((string)Application.Current.FindResource("StringPasswordLengthError"));

                player.Password = newPassword;
            }

            // Оновлення інформації про гравця в репозиторії
            _playerRepository.UpdatePlayer(player);
        }

        /// <summary>
        /// Логічно видаляє гравця.
        /// </summary>
        /// <param name="playerId">Ідентифікатор гравця.</param>
        public void LogicalDeletePlayer(int playerId)
        {
            // Отримання інформації про гравця за ідентифікатором
            var player = _playerRepository.GetPlayerById(playerId);
            if (player == null || player.IsDeleted)
                throw new ArgumentException((string)Application.Current.FindResource("StringUserNotFoundOrIsDeletedError"));

            // Логічне видалення гравця
            _playerRepository.DeletePlayer(playerId);
        }

        /// <summary>
        /// Аутентифікація гравця.
        /// </summary>
        /// <param name="username">Ім'я користувача.</param>
        /// <param name="password">Пароль користувача.</param>
        /// <returns>Інформація про аутентифікованого гравця.</returns>
        public Player Login(string username, string password)
        {
            // Отримання інформації про гравця за ім'ям користувача
            var player = _playerRepository.GetPlayerByUsername(username);

            // Перевірка на порожні рядки
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException((string)Application.Current.FindResource("StringUsernamePasswordEmptyError"));

            // Перевірка на коректність даних
            if (player == null || player.IsDeleted || player.Username != username || player.Password != password)
                throw new ArgumentException((string)Application.Current.FindResource("StringWrongUsernameOrPaswordError"));

            // Оновлення часу останнього входу та інформації про гравця в репозиторії
            player.LastLogin = DateTime.Now;
            _playerRepository.UpdatePlayer(player);
            return player;
        }

        /// <summary>
        /// Отримує інформацію про гравця за ідентифікатором.
        /// </summary>
        /// <param name="playerId">Ідентифікатор гравця.</param>
        /// <returns>Інформація про гравця.</returns>
        public Player GetPlayerById(int playerId)
        {
            var player = _playerRepository.GetPlayerById(playerId);
            if (player == null || player.IsDeleted)
                throw new ArgumentException((string)Application.Current.FindResource("StringUserNotFoundOrIsDeletedError"));

            return player;
        }

        /// <summary>
        /// Отримує інформацію про гравця за ім'ям користувача.
        /// </summary>
        /// <param name="username">Ім'я користувача.</param>
        /// <returns>Інформація про гравця.</returns>
        public Player GetPlayerByUsername(string username)
        {
            // Отримання інформації про гравця за ім'ям користувача
            var player = _playerRepository.GetPlayerByUsername(username);
            if (player == null || player.IsDeleted)
                throw new ArgumentException((string)Application.Current.FindResource("StringWrongUsernameError"));

            // Оновлення часу останнього входу та інформації про гравця в репозиторії
            player.LastLogin = DateTime.Now;
            _playerRepository.UpdatePlayer(player);
            return player;
        }

        /// <summary>
        /// Отримує список топ-N гравців за кількістю перемог.
        /// </summary>
        /// <param name="count">Кількість гравців, яких потрібно повернути.</param>
        /// <returns>Список топ-N гравців.</returns>
        public List<Player> GetTopPlayers(int count)
        {
            // Отримання списку всіх гравців
            var allPlayers = _playerRepository.GetTopPlayers(count);

            // Сортування гравців за кількістю перемог у спадаючому порядку та вибір перших count гравців
            var topPlayers = allPlayers.OrderByDescending(player => player.Wins).Take(count).ToList();

            return topPlayers;
        }
    }
}
