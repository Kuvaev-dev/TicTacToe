using System;

namespace TicTacToe.Models
{
    /// <summary>
    /// Представляет модель игры в крестики-нолики.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Уникальный идентификатор игры.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор игрока, который сыграл эту игру.
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// Результат игры. 0 - поражение, 1 - ничья, 2 - победа.
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// Дата и время окончания игры.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Ходы, сделанные в игре (возможно, в виде строки или другого формата).
        /// </summary>
        public string Moves { get; set; }
    }
}
