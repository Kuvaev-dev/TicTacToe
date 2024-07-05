using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    /// <summary>
    /// Клас, що забезпечує доступ до даних про гравців у базі даних.
    /// </summary>
    public class PlayerRepository : IPlayerRepository
    {
        private readonly string _connectionString;

        public PlayerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Додає нового гравця до бази даних.
        /// </summary>
        /// <param name="player">Інформація про додаваного гравця.</param>
        public void AddPlayer(Player player)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Players (Username, Password, Salt, IsDeleted, GamesPlayed, Wins, Losses, Draws, LastLogin) VALUES (@Username, @Password, @Salt, @IsDeleted, @GamesPlayed, @Wins, @Losses, @Draws, @LastLogin)", connection);
                command.Parameters.AddWithValue("@Username", player.Username);
                command.Parameters.AddWithValue("@Password", player.Password);
                command.Parameters.AddWithValue("@Salt", player.Salt);
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
        /// Оновлює інформацію про гравця в базі даних.
        /// </summary>
        /// <param name="player">Інформація про оновлюваного гравця.</param>
        public void UpdatePlayer(Player player)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Players SET Username = @Username, Password = @Password, Salt = @Salt, IsDeleted = @IsDeleted, GamesPlayed = @GamesPlayed, Wins = @Wins, Losses = @Losses, Draws = @Draws, LastLogin = @LastLogin WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", player.Id);
                command.Parameters.AddWithValue("@Username", player.Username);
                command.Parameters.AddWithValue("@Password", player.Password);
                command.Parameters.AddWithValue("@Salt", player.Salt);
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
        /// Логічно видаляє гравця з бази даних за його ідентифікатором.
        /// </summary>
        /// <param name="playerId">Ідентифікатор гравця.</param>
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
        /// Отримує інформацію про гравця за його ідентифікатором.
        /// </summary>
        /// <param name="playerId">Ідентифікатор гравця.</param>
        /// <returns>Об'єкт Player, що представляє знайденого гравця.</returns>
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
                            Salt = (string)reader["Salt"],
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
        /// Отримує інформацію про гравця за його іменем користувача.
        /// </summary>
        /// <param name="username">Ім'я користувача гравця.</param>
        /// <returns>Об'єкт Player, що представляє знайденого гравця.</returns>
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
                            Salt = (string)reader["Salt"],
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
        /// Отримує список кращих гравців за кількістю перемог.
        /// </summary>
        /// <param name="count">Кількість гравців, яких потрібно отримати.</param>
        /// <returns>Список об'єктів Player, що представляють кращих гравців.</returns>
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