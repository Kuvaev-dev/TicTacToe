using System.Collections.Generic;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    public interface IGameRepository
    {
        void AddGame(Game game);
        IEnumerable<Game> GetGamesByPlayer(int playerId);
    }
}
