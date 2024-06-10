using System;

namespace TicTacToe.DataAccess.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int? WinnerId { get; set; }
        public DateTime Date { get; set; }
    }
}