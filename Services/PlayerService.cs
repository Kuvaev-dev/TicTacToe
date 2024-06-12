using System;
using TicTacToe.Models;
using TicTacToe.Repositories;

namespace TicTacToe.Services
{
    public class PlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public void RegisterPlayer(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Username and password cannot be empty.");

            if (password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters long.");

            var existingPlayer = _playerRepository.GetPlayerByUsername(username);
            if (existingPlayer != null)
                throw new ArgumentException("Username already exists.");

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

            _playerRepository.AddPlayer(player);
        }

        public void UpdateProfile(int playerId, string newUsername, string newPassword)
        {
            var player = _playerRepository.GetPlayerById(playerId);
            if (player == null || player.IsDeleted)
                throw new ArgumentException("Player not found or is deleted.");

            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                var existingPlayer = _playerRepository.GetPlayerByUsername(newUsername);
                if (existingPlayer != null && existingPlayer.Id != playerId)
                    throw new ArgumentException("Username already exists.");

                player.Username = newUsername;
            }

            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                if (newPassword.Length < 6)
                    throw new ArgumentException("Password must be at least 6 characters long.");

                player.Password = newPassword;
            }

            _playerRepository.UpdatePlayer(player);
        }

        public void LogicalDeletePlayer(int playerId)
        {
            var player = _playerRepository.GetPlayerById(playerId);
            if (player == null || player.IsDeleted)
                throw new ArgumentException("Player not found or is already deleted.");

            _playerRepository.DeletePlayer(playerId);
        }

        public Player Login(string username, string password)
        {
            var player = _playerRepository.GetPlayerByUsername(username);
            if (player == null || player.IsDeleted || player.Password != password)
                throw new ArgumentException("Invalid username or password.");

            player.LastLogin = DateTime.Now;
            _playerRepository.UpdatePlayer(player);
            return player;
        }

        public Player GetPlayerByUsername(string username)
        {
            var player = _playerRepository.GetPlayerByUsername(username);
            if (player == null || player.IsDeleted)
                throw new ArgumentException("Invalid username.");

            player.LastLogin = DateTime.Now;
            _playerRepository.UpdatePlayer(player);
            return player;
        }
    }
}
