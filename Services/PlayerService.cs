using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models;
using TicTacToe.Repositories;

namespace TicTacToe.Services
{
    /// <summary>
    /// Сервис, отвечающий за управление игроками.
    /// </summary>
    public class PlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        /// <summary>
        /// Регистрирует нового игрока.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        public void RegisterPlayer(string username, string password)
        {
            // Проверка на пустые строки
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Username and password cannot be empty.");

            // Проверка на длину пароля
            if (password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters long.");

            // Проверка на существование пользователя с таким именем
            var existingPlayer = _playerRepository.GetPlayerByUsername(username);
            if (existingPlayer != null)
                throw new ArgumentException("Username already exists.");

            // Создание нового игрока
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

            // Добавление игрока в репозиторий
            _playerRepository.AddPlayer(player);
        }

        /// <summary>
        /// Обновляет профиль игрока.
        /// </summary>
        /// <param name="playerId">Идентификатор игрока.</param>
        /// <param name="newUsername">Новое имя пользователя.</param>
        /// <param name="newPassword">Новый пароль пользователя.</param>
        public void UpdateProfile(int playerId, string newUsername, string newPassword)
        {
            // Получение информации об игроке по идентификатору
            var player = _playerRepository.GetPlayerById(playerId);
            if (player == null || player.IsDeleted)
                throw new ArgumentException("Player not found or is deleted.");

            // Проверка и обновление имени пользователя
            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                var existingPlayer = _playerRepository.GetPlayerByUsername(newUsername);
                if (existingPlayer != null && existingPlayer.Id != playerId)
                    throw new ArgumentException("Username already exists.");

                player.Username = newUsername;
            }

            // Проверка и обновление пароля пользователя
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                if (newPassword.Length < 6)
                    throw new ArgumentException("Password must be at least 6 characters long.");

                player.Password = newPassword;
            }

            // Обновление информации об игроке в репозитории
            _playerRepository.UpdatePlayer(player);
        }

        /// <summary>
        /// Логически удаляет игрока.
        /// </summary>
        /// <param name="playerId">Идентификатор игрока.</param>
        public void LogicalDeletePlayer(int playerId)
        {
            // Получение информации об игроке по идентификатору
            var player = _playerRepository.GetPlayerById(playerId);
            if (player == null || player.IsDeleted)
                throw new ArgumentException("Player not found or is already deleted.");

            // Логическое удаление игрока
            _playerRepository.DeletePlayer(playerId);
        }

        /// <summary>
        /// Аутентификация игрока.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Информация об аутентифицированном игроке.</returns>
        public Player Login(string username, string password)
        {
            // Получение информации об игроке по имени пользователя
            var player = _playerRepository.GetPlayerByUsername(username);
            if (player == null || player.IsDeleted || player.Password != password)
                throw new ArgumentException("Invalid username or password.");

            // Обновление времени последнего входа и информации об игроке в репозитории
            player.LastLogin = DateTime.Now;
            _playerRepository.UpdatePlayer(player);
            return player;
        }

        /// <summary>
        /// Получает информацию об игроке по имени пользователя.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <returns>Информация об игроке.</returns>
        public Player GetPlayerByUsername(string username)
        {
            // Получение информации об игроке по имени пользователя
            var player = _playerRepository.GetPlayerByUsername(username);
            if (player == null || player.IsDeleted)
                throw new ArgumentException("Invalid username.");

            // Обновление времени последнего входа и информации об игроке в репозитории
            player.LastLogin = DateTime.Now;
            _playerRepository.UpdatePlayer(player);
            return player;
        }

        public List<Player> GetTopPlayers(int count)
        {
            // Получаем список всех игроков
            var allPlayers = _playerRepository.GetTopPlayers(count);

            // Сортируем игроков по количеству побед в убывающем порядке и выбираем первые count игроков
            var topPlayers = allPlayers.OrderByDescending(player => player.Wins).Take(count).ToList();

            return topPlayers;
        }
    }
}