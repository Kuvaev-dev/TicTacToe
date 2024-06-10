using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Users WHERE Id = @Id AND IsDeleted = 0", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = (int)reader["Id"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            IsDeleted = (bool)reader["IsDeleted"],
                            LastLogin = reader["LastLogin"] as DateTime?
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Users WHERE IsDeleted = 0", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = (int)reader["Id"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            IsDeleted = (bool)reader["IsDeleted"],
                            LastLogin = reader["LastLogin"] as DateTime?
                        });
                    }
                }
            }
            return users;
        }

        public void Add(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Users (Username, Password, IsDeleted, LastLogin) VALUES (@Username, @Password, @IsDeleted, @LastLogin)", connection);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@IsDeleted", user.IsDeleted);
                command.Parameters.AddWithValue("@LastLogin", user.LastLogin ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        public void Update(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Users SET Username = @Username, Password = @Password, IsDeleted = @IsDeleted, LastLogin = @LastLogin WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", user.Id);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@IsDeleted", user.IsDeleted);
                command.Parameters.AddWithValue("@LastLogin", user.LastLogin ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Users SET IsDeleted = 1 WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}
