using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    /// <summary>
    /// Репозиторий для работы с данными об играх в базе данных.
    /// </summary>
    public class GameRepository : IGameRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Инициализирует новый экземпляр класса GameRepository с указанным строковым подключением к базе данных.
        /// </summary>
        /// <param name="connectionString">Строковое подключение к базе данных.</param>
        public GameRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Добавляет новую игру в базу данных.
        /// </summary>
        /// <param name="game">Информация о добавляемой игре.</param>
        public void AddGame(Game game)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Games (PlayerId, Result, Date, Moves) VALUES (@PlayerId, @Result, @Date, @Moves)", connection);
                command.Parameters.AddWithValue("@PlayerId", game.PlayerId);
                command.Parameters.AddWithValue("@Result", game.Result);
                command.Parameters.AddWithValue("@Date", game.Date);
                command.Parameters.AddWithValue("@Moves", game.Moves);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Получает список игр, сыгранных игроком с указанным идентификатором.
        /// </summary>
        /// <param name="playerId">Идентификатор игрока.</param>
        /// <returns>Список объектов Game, представляющих игры игрока.</returns>
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
                            Moves = (string)reader["Moves"]
                        });
                    }
                }
            }
            return games;
        }
    }
}