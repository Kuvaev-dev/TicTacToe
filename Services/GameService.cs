using System;
using TicTacToe.Bots;
using TicTacToe.Repositories;

namespace TicTacToe.Services
{
    public class GameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private char[,] _board;
        private char _currentPlayer;
        private IBot _bot;

        public GameService(IGameRepository gameRepository, IPlayerRepository playerRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _board = new char[3, 3];
            _currentPlayer = 'X'; // 'X' for player, 'O' for bot
        }

        public void StartNewGame(string botLevel)
        {
            _board = new char[3, 3];
            _currentPlayer = new Random().Next(0, 2) == 0 ? 'X' : 'O';

            switch (botLevel)
            {
                case "НОВИЧОК":
                    _bot = new SimpleBot();
                    break;
                case "ЗАЩИТА":
                    _bot = new DefensiveBot();
                    break;
                case "НАПАДЕНИЕ":
                    _bot = new OffensiveBot();
                    break;
                case "ГУРУ":
                    _bot = new GuruBot();
                    break;
                case "ИИ":
                    _bot = new AIBot();
                    break;
                default:
                    _bot = new SimpleBot();
                    break;
            }
        }

        public char GetCurrentPlayer() => _currentPlayer;

        public bool MakeMove(int row, int col)
        {
            if (_board[row, col] == '\0')
            {
                _board[row, col] = _currentPlayer;
                if (CheckWinner())
                {
                    UpdateGameStats(_currentPlayer == 'X' ? "Player" : "Bot");
                    return true;
                }
                _currentPlayer = _currentPlayer == 'X' ? 'O' : 'X';

                if (_currentPlayer == 'O')
                {
                    var move = _bot.GetNextMove(_board);
                    _board[move.row, move.col] = 'O';
                    if (CheckWinner())
                    {
                        UpdateGameStats(_currentPlayer == 'X' ? "Player" : "Bot");
                        return true;
                    }
                    _currentPlayer = 'X';
                }
            }
            return false;
        }

        public (int row, int col) GetHint()
        {
            return _bot.GetNextMove(_board);
        }

        public bool CheckWinner()
        {
            // Check rows, columns and diagonals
            for (int i = 0; i < 3; i++)
            {
                if (_board[i, 0] != '\0' && _board[i, 0] == _board[i, 1] && _board[i, 1] == _board[i, 2])
                    return true;
                if (_board[0, i] != '\0' && _board[0, i] == _board[1, i] && _board[1, i] == _board[2, i])
                    return true;
            }

            if (_board[0, 0] != '\0' && _board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2])
                return true;
            if (_board[0, 2] != '\0' && _board[0, 2] == _board[1, 1] && _board[1, 1] == _board[2, 0])
                return true;

            return false;
        }

        private void UpdateGameStats(string winner)
        {
            // Update player and bot statistics in the database
            if (winner == "Player")
            {
                var player = _playerRepository.GetPlayerById(1); // Example player ID
                player.Wins++;
                player.GamesPlayed++;
                _playerRepository.UpdatePlayer(player);
            }
            else if (winner == "Bot")
            {
                var botPlayer = _playerRepository.GetPlayerById(2); // Example bot player ID
                botPlayer.Wins++;
                botPlayer.GamesPlayed++;
                _playerRepository.UpdatePlayer(botPlayer);
            }
        }

        public char[,] GetBoard() => _board;
    }
}
