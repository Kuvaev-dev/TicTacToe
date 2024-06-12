﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    /// <summary>
    /// Класс, отвечающий за доступ к данным об игроках в базе данных.
    /// </summary>
    public class PlayerRepository : IPlayerRepository
    {
        private readonly string _connectionString;

        public PlayerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Добавляет нового игрока в базу данных.
        /// </summary>
        /// <param name="player">Информация о добавляемом игроке.</param>
        public void AddPlayer(Player player)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Players (Username, Password, IsDeleted, GamesPlayed, Wins, Losses, Draws, LastLogin) VALUES (@Username, @Password, @IsDeleted, @GamesPlayed, @Wins, @Losses, @Draws, @LastLogin)", connection);
                command.Parameters.AddWithValue("@Username", player.Username);
                command.Parameters.AddWithValue("@Password", player.Password);
                command.Parameters.AddWithValue("@IsDeleted", player.IsDeleted);
                command.Parameters.AddWithValue("@GamesPlayed", player.GamesPlayed);
                command.Parameters.AddWithValue("@Wins", player.Wins);
                command.Parameters.AddWithValue("@Losses", player.Losses);
                command.Parameters.AddWithValue("@Draws", player.Draws);
                command.Parameters.AddWithValue("@LastLogin", (object)player.LastLogin ?? DBNull.Value);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Обновляет информацию об игроке в базе данных.
        /// </summary>
        /// <param name="player">Информация об обновляемом игроке.</param>
        public void UpdatePlayer(Player player)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Players SET Username = @Username, Password = @Password, IsDeleted = @IsDeleted, GamesPlayed = @GamesPlayed, Wins = @Wins, Losses = @Losses, Draws = @Draws, LastLogin = @LastLogin WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", player.Id);
                command.Parameters.AddWithValue("@Username", player.Username);
                command.Parameters.AddWithValue("@Password", player.Password);
                command.Parameters.AddWithValue("@IsDeleted", player.IsDeleted);
                command.Parameters.AddWithValue("@GamesPlayed", player.GamesPlayed);
                command.Parameters.AddWithValue("@Wins", player.Wins);
                command.Parameters.AddWithValue("@Losses", player.Losses);
                command.Parameters.AddWithValue("@Draws", player.Draws);
                command.Parameters.AddWithValue("@LastLogin", (object)player.LastLogin ?? DBNull.Value);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Логически удаляет игрока из базы данных по его идентификатору.
        /// </summary>
        /// <param name="playerId">Идентификатор игрока.</param>
        public void DeletePlayer(int playerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Players SET IsDeleted = 1 WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", playerId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Получает информацию об игроке по его идентификатору.
        /// </summary>
        /// <param name="playerId">Идентификатор игрока.</param>
        /// <returns>Объект Player, представляющий найденного игрока.</returns>
        public Player GetPlayerById(int playerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Players WHERE Id = @Id AND IsDeleted = 0", connection);
                command.Parameters.AddWithValue("@Id", playerId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Player
                        {
                            Id = (int)reader["Id"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            IsDeleted = (bool)reader["IsDeleted"],
                            GamesPlayed = (int)reader["GamesPlayed"],
                            Wins = (int)reader["Wins"],
                            Losses = (int)reader["Losses"],
                            Draws = (int)reader["Draws"],
                            LastLogin = reader["LastLogin"] as DateTime?
                        };
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Получает информацию об игроке по его имени пользователя.
        /// </summary>
        /// <param name="username">Имя пользователя игрока.</param>
        /// <returns>Объект Player, представляющий найденного игрока.</returns>
        public Player GetPlayerByUsername(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Players WHERE Username = @Username AND IsDeleted = 0", connection);
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Player
                        {
                            Id = (int)reader["Id"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            IsDeleted = (bool)reader["IsDeleted"],
                            GamesPlayed = (int)reader["GamesPlayed"],
                            Wins = (int)reader["Wins"],
                            Losses = (int)reader["Losses"],
                            Draws = (int)reader["Draws"],
                            LastLogin = reader["LastLogin"] as DateTime?
                        };
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Получает список лучших игроков по количеству побед.
        /// </summary>
        /// <param name="count">Количество игроков, которые нужно получить.</param>
        /// <returns>Список объектов Player, представляющих лучших игроков.</returns>
        public IEnumerable<Player> GetTopPlayers(int count)
        {
            var players = new List<Player>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT TOP (@Count) * FROM Players WHERE IsDeleted = 0 ORDER BY Wins DESC, Draws DESC, Losses ASC", connection);
                command.Parameters.AddWithValue("@Count", count);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        players.Add(new Player
                        {
                            Id = (int)reader["Id"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            IsDeleted = (bool)reader["IsDeleted"],
                            GamesPlayed = (int)reader["GamesPlayed"],
                            Wins = (int)reader["Wins"],
                            Losses = (int)reader["Losses"],
                            Draws = (int)reader["Draws"],
                            LastLogin = reader["LastLogin"] as DateTime?
                        });
                    }
                }
            }
            return players;
        }
    }
}
