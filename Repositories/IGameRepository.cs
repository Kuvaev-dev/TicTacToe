using System.Collections.Generic;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    /// <summary>
    /// Интерфейс для работы с данными об играх в базе данных.
    /// </summary>
    public interface IGameRepository
    {
        /// <summary>
        /// Добавляет новую игру в базу данных.
        /// </summary>
        /// <param name="game">Информация о добавляемой игре.</param>
        void AddGame(Game game);

        /// <summary>
        /// Получает список игр, сыгранных игроком с указанным идентификатором.
        /// </summary>
        /// <param name="playerId">Идентификатор игрока.</param>
        /// <returns>Список объектов Game, представляющих игры игрока.</returns>
        IEnumerable<Game> GetGamesByPlayer(int playerId);
    }
}