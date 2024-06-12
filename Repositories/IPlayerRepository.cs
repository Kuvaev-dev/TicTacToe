using System.Collections.Generic;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    /// <summary>
    /// Интерфейс для работы с данными об игроках в базе данных.
    /// </summary>
    public interface IPlayerRepository
    {
        /// <summary>
        /// Добавляет нового игрока в базу данных.
        /// </summary>
        /// <param name="player">Информация о добавляемом игроке.</param>
        void AddPlayer(Player player);

        /// <summary>
        /// Обновляет информацию об игроке в базе данных.
        /// </summary>
        /// <param name="player">Информация об обновляемом игроке.</param>
        void UpdatePlayer(Player player);

        /// <summary>
        /// Логически удаляет игрока из базы данных по его идентификатору.
        /// </summary>
        /// <param name="playerId">Идентификатор игрока.</param>
        void DeletePlayer(int playerId);

        /// <summary>
        /// Получает информацию об игроке по его идентификатору.
        /// </summary>
        /// <param name="playerId">Идентификатор игрока.</param>
        /// <returns>Объект Player, представляющий найденного игрока.</returns>
        Player GetPlayerById(int playerId);

        /// <summary>
        /// Получает информацию об игроке по его имени пользователя.
        /// </summary>
        /// <param name="username">Имя пользователя игрока.</param>
        /// <returns>Объект Player, представляющий найденного игрока.</returns>
        Player GetPlayerByUsername(string username);

        /// <summary>
        /// Получает список лучших игроков по количеству побед.
        /// </summary>
        /// <param name="count">Количество игроков, которые нужно получить.</param>
        /// <returns>Список объектов Player, представляющих лучших игроков.</returns>
        IEnumerable<Player> GetTopPlayers(int count);
    }
}
