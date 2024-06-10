using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe.Repositories;

namespace TicTacToe.Services
{
    public class GameService
    {
        private readonly GameRepository _gameRepository;
        private readonly RatingRepository _ratingRepository;

        public GameService(GameRepository gameRepository, RatingRepository ratingRepository)
        {
            _gameRepository = gameRepository;
            _ratingRepository = ratingRepository;
        }

        public void RecordGame(int player1Id, int player2Id, int? winnerId)
        {
            var game = new Game
            {
                Player1Id = player1Id,
                Player2Id = player2Id,
                WinnerId = winnerId,
                Date = DateTime.Now
            };
            _gameRepository.Add(game);
            UpdateRatings(player1Id, player2Id, winnerId);
        }

        private void UpdateRatings(int player1Id, int player2Id, int? winnerId)
        {
            var player1Rating = _ratingRepository.GetById(player1Id) ?? new Rating { PlayerId = player1Id };
            var player2Rating = _ratingRepository.GetById(player2Id) ?? new Rating { PlayerId = player2Id };
            player1Rating.TotalGames++;
            player2Rating.TotalGames++;
            if (winnerId == player1Id)
            {
                player1Rating.Wins++;
                player2Rating.Losses++;
            }
            else if (winnerId == player2Id)
            {
                player2Rating.Wins++;
                player1Rating.Losses++;
            }
            else
            {
                player1Rating.Draws++;
                player2Rating.Draws++;
            }
            _ratingRepository.Update(player1Rating);
            _ratingRepository.Update(player2Rating);
        }

        public IEnumerable<Rating> GetTopRatings()
        {
            var ratings = _ratingRepository.GetAll();
            return ratings.OrderByDescending(r => r.Wins)
            .ThenByDescending(r => r.Draws)
            .ThenBy(r => r.Losses)
            .Take(5);
        }

        public Rating GetCurrentRating(int playerId)
        {
            return _ratingRepository.GetById(playerId);
        }
    }
}
