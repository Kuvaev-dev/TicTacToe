using System;

namespace TicTacToe.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int Result { get; set; } // 0: Loss, 1: Draw, 2: Win
        public DateTime Date { get; set; }
        public string Moves { get; set; }
    }
}
