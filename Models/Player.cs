using System;

namespace TicTacToe.Models
{
    /// <summary>
    /// Представляет модель игрока в игре крестики-нолики.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Уникальный идентификатор игрока.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя игрока.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль игрока.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Флаг, указывающий, удален ли игрок (логическое удаление).
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Количество сыгранных игр игроком.
        /// </summary>
        public int GamesPlayed { get; set; }

        /// <summary>
        /// Количество побед игрока.
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Количество поражений игрока.
        /// </summary>
        public int Losses { get; set; }

        /// <summary>
        /// Количество ничьих игрока.
        /// </summary>
        public int Draws { get; set; }

        /// <summary>
        /// Дата и время последнего входа в систему игрока.
        /// </summary>
        public DateTime? LastLogin { get; set; }
    }
}