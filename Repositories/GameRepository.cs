using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly string _connectionString;

        public GameRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

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
