using System.Collections.Generic;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    /// <summary>
    /// Інтерфейс для роботи з даними про ігри в базі даних.
    /// </summary>
    public interface IGameRepository
    {
        /// <summary>
        /// Додає нову гру в базу даних.
        /// </summary>
        /// <param name="game">Інформація про додавану гру.</param>
        void AddGame(Game game);

        /// <summary>
        /// Отримує список ігор, зіграних гравцем з вказаним ідентифікатором.
        /// </summary>
        /// <param name="playerId">Ідентифікатор гравця.</param>
        /// <returns>Список об'єктів Game, які представляють ігри гравця.</returns>
        IEnumerable<Game> GetGamesByPlayer(int playerId);
    }
}