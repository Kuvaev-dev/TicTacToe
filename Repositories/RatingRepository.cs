using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    public class RatingRepository : IRepository<Rating>
    {
        private readonly string _connectionString;
        public RatingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Rating GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Ratings WHERE PlayerId = @PlayerId", connection);
                command.Parameters.AddWithValue("@PlayerId", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Rating
                        {
                            PlayerId = (int)reader["PlayerId"],
                            Wins = (int)reader["Wins"],
                            Losses = (int)reader["Losses"],
                            Draws = (int)reader["Draws"]
                        };
                    }
                }
                return null;
            }
        }
        public IEnumerable<Rating> GetAll()
        {
            var ratings = new List<Rating>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Ratings", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ratings.Add(new Rating
                        {
                            PlayerId = (int)reader["PlayerId"],
                            Wins = (int)reader["Wins"],
                            Losses = (int)reader["Losses"],
                            Draws = (int)reader["Draws"]
                        });
                    }
                }
            }
            return ratings;
        }
        public void Add(Rating rating)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Ratings (PlayerId, Wins, Losses, Draws) VALUES (@PlayerId, @Wins, @Losses, @Draws)", connection);
                command.Parameters.AddWithValue("@PlayerId", rating.PlayerId);
                command.Parameters.AddWithValue("@Wins", rating.Wins);
                command.Parameters.AddWithValue("@Losses", rating.Losses);
                command.Parameters.AddWithValue("@Draws", rating.Draws);
                command.ExecuteNonQuery();
            }
        }
        public void Update(Rating rating)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Ratings SET Wins = @Wins, Losses = @Losses, Draws = @Draws WHERE PlayerId = @PlayerId", connection);
                command.Parameters.AddWithValue("@PlayerId", rating.PlayerId);
                command.Parameters.AddWithValue("@Wins", rating.Wins);
                command.Parameters.AddWithValue("@Losses", rating.Losses);
                command.Parameters.AddWithValue("@Draws", rating.Draws);
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            throw new NotImplementedException("Delete method is not implemented for RatingRepository");
        }
    }
}
