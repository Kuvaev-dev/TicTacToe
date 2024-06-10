using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private readonly string _connectionString;

        public GameRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Game GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Games WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Game
                        {
                            Id = (int)reader["Id"],
                            Player1Id = (int)reader["Player1Id"],
                            Player2Id = (int)reader["Player2Id"],
                            WinnerId = reader["WinnerId"] as int?,
                            Date = (DateTime)reader["Date"]
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<Game> GetAll()
        {
            var games = new List<Game>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Games", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        games.Add(new Game
                        {
                            Id = (int)reader["Id"],
                            Player1Id = (int)reader["Player1Id"],
                            Player2Id = (int)reader["Player2Id"],
                            WinnerId = reader["WinnerId"] as int?,
                            Date = (DateTime)reader["Date"]
                        });
                    }
                }
            }
            return games;
        }

        public void Add(Game game)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Games (Player1Id, Player2Id, WinnerId, Date) VALUES (@Player1Id, @Player2Id, @WinnerId, @Date)", connection);
                command.Parameters.AddWithValue("@Player1Id", game.Player1Id);
                command.Parameters.AddWithValue("@Player2Id", game.Player2Id);
                command.Parameters.AddWithValue("@WinnerId", (object)game.WinnerId ?? DBNull.Value);
                command.Parameters.AddWithValue("@Date", game.Date);

                command.ExecuteNonQuery();
            }
        }

        public void Update(Game game)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Games SET Player1Id = @Player1Id, Player2Id = @Player2Id, WinnerId = @WinnerId, Date = @Date WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", game.Id);
                command.Parameters.AddWithValue("@Player1Id", game.Player1Id);
                command.Parameters.AddWithValue("@Player2Id", game.Player2Id);
                command.Parameters.AddWithValue("@WinnerId", (object)game.WinnerId ?? DBNull.Value);
                command.Parameters.AddWithValue("@Date", game.Date);

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Games WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}
