using System.Collections.Generic;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    public interface IPlayerRepository
    {
        void AddPlayer(Player player);
        void UpdatePlayer(Player player);
        void DeletePlayer(int playerId);
        Player GetPlayerById(int playerId);
        Player GetPlayerByUsername(string username);
        IEnumerable<Player> GetTopPlayers(int count);
    }
}
