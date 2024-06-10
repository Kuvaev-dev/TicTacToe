using System;
using System.Linq;
using TicTacToe.Models;
using TicTacToe.Repositories;

namespace TicTacToe.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RegisterUser(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = password,
                LastLogin = DateTime.Now
            };
            _userRepository.Add(user);
        }

        public User AuthenticateUser(string username, string password)
        {
            var users = _userRepository.GetAll();
            return users.FirstOrDefault(u => u.Username == username && u.Password == password && !u.IsDeleted);
        }

        public void UpdateUser(int userId, string newUsername, string newPassword)
        {
            var user = _userRepository.GetById(userId);
            if (user != null && !user.IsDeleted)
            {
                user.Username = newUsername;
                user.Password = newPassword;
                _userRepository.Update(user);
            }
        }

        public void DeleteUser(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user != null && !user.IsDeleted && user.LastLogin < DateTime.Now.AddMonths(-2))
            {
                user.IsDeleted = true;
                _userRepository.Update(user);
            }
        }
    }
}
