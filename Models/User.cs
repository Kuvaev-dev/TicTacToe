using System;

namespace TicTacToe.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
