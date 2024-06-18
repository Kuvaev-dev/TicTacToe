using System.Collections.Generic;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    /// <summary>
    /// Інтерфейс для роботи з даними про гравців в базі даних.
    /// </summary>
    public interface IPlayerRepository
    {
        /// <summary>
        /// Додає нового гравця в базу даних.
        /// </summary>
        /// <param name="player">Інформація про додаваного гравця.</param>
        void AddPlayer(Player player);

        /// <summary>
        /// Оновлює інформацію про гравця в базі даних.
        /// </summary>
        /// <param name="player">Інформація про оновлюваного гравця.</param>
        void UpdatePlayer(Player player);

        /// <summary>
        /// Логічно видаляє гравця з бази даних за його ідентифікатором.
        /// </summary>
        /// <param name="playerId">Ідентифікатор гравця.</param>
        void DeletePlayer(int playerId);

        /// <summary>
        /// Отримує інформацію про гравця за його ідентифікатором.
        /// </summary>
        /// <param name="playerId">Ідентифікатор гравця.</param>
        /// <returns>Об'єкт Player, який представляє знайденого гравця.</returns>
        Player GetPlayerById(int playerId);

        /// <summary>
        /// Отримує інформацію про гравця за його іменем користувача.
        /// </summary>
        /// <param name="username">Ім'я користувача гравця.</param>
        /// <returns>Об'єкт Player, який представляє знайденого гравця.</returns>
        Player GetPlayerByUsername(string username);

        /// <summary>
        /// Отримує список кращих гравців за кількістю перемог.
        /// </summary>
        /// <param name="count">Кількість гравців, яких потрібно отримати.</param>
        /// <returns>Список об'єктів Player, які представляють кращих гравців.</returns>
        IEnumerable<Player> GetTopPlayers(int count);
    }
}