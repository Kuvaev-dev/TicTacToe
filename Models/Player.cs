using System;

namespace TicTacToe.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
