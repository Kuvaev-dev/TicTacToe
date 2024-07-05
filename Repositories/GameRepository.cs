using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    /// <summary>
    /// Репозиторій для роботи з даними про ігри в базі даних.
    /// </summary>
    public class GameRepository : IGameRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Ініціалізує новий екземпляр класу GameRepository з вказаним рядком підключення до бази даних.
        /// </summary>
        /// <param name="connectionString">Рядок підключення до бази даних.</param>
        public GameRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Додає нову гру в базу даних.
        /// </summary>
        /// <param name="game">Інформація про додавану гру.</param>
        public void AddGame(Game game)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Games (PlayerId, Result, Date, Moves, BotLevel) VALUES (@PlayerId, @Result, @Date, @Moves, @BotLevel)", connection);
                command.Parameters.AddWithValue("@PlayerId", game.PlayerId);
                command.Parameters.AddWithValue("@Result", game.Result);
                command.Parameters.AddWithValue("@Date", game.Date);
                command.Parameters.AddWithValue("@Moves", game.Moves);
                command.Parameters.AddWithValue("@BotLevel", game.BotLevel);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Отримує список ігор, зіграних гравцем з вказаним ідентифікатором.
        /// </summary>
        /// <param name="playerId">Ідентифікатор гравця.</param>
        /// <returns>Список об'єктів Game, які представляють ігри гравця.</returns>
        public IEnumerable<Game> GetGamesByPlayer(int playerId)
        {
            var games = new List<Game>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Games WHERE PlayerId = @PlayerId", connection);
                command.Parameters.AddWithValue("@PlayerId", playerId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        games.Add(new Game
                        {
                            Id = (int)reader["Id"],
                            PlayerId = (int)reader["PlayerId"],
                            Result = (int)reader["Result"],
                            Date = (DateTime)reader["Date"],
                            Moves = (string)reader["Moves"],
                            BotLevel = (string)reader["BotLevel"]
                        });
                    }
                }
            }
            return games;
        }
    }
}