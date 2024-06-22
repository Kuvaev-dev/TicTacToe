using System;
using System.Windows;
using TicTacToe.Bots;
using TicTacToe.Repositories;
using TicTacToe.Views;

namespace TicTacToe.Services
{
    /// <summary>
    /// Сервіс для керування грою та взаємодії з гравцями та ботами.
    /// </summary>
    public class GameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private char[,] _board;
        private char _currentPlayer;
        private IBot _bot;

        /// <summary>
        /// Ініціалізує новий екземпляр класу GameService з вказаними репозиторіями.
        /// </summary>
        /// <param name="gameRepository">Репозиторій для роботи з даними про ігри.</param>
        /// <param name="playerRepository">Репозиторій для роботи з даними про гравців.</param>
        public GameService(IGameRepository gameRepository, IPlayerRepository playerRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _board = new char[3, 3];
            _currentPlayer = 'X'; // 'X' для гравця, 'O' для бота
        }

        /// <summary>
        /// Розпочинає нову гру з вказаним рівнем складності бота.
        /// </summary>
        /// <param name="botLevel">Рівень складності бота.</param>
        public void StartNewGame(string botLevel)
        {
            _board = new char[3, 3];
            _currentPlayer = 'X'; // Гравець завжди починає гру з хрестика

            _bot = botLevel switch
            {
                "Новачок" => new SimpleBot(),
                "Захист" => new DefensiveBot(),
                "Напад" => new OffensiveBot(),
                "Гуру" => new GuruBot(),
                "ШІ" => new AIBot(),
                _ => new SimpleBot(),
            };
        }

        /// <summary>
        /// Повертає поточного гравця (X - гравець, O - бот).
        /// </summary>
        public char GetCurrentPlayer() => _currentPlayer;

        /// <summary>
        /// Виконує хід у вказану клітинку ігрового поля.
        /// </summary>
        /// <param name="row">Номер рядка.</param>
        /// <param name="col">Номер стовпця.</param>
        /// <returns>True, якщо хід виконаний успішно, інакше false.</returns>
        public bool MakeMove(int row, int col)
        {
            if (row < 0 || row >= 3 || col < 0 || col >= 3 || _board[row, col] != '\0')
            {
                // Перевірка коректності ходу
                return false;
            }

            _board[row, col] = _currentPlayer;

            if (CheckWinner())
            {
                UpdateGameStats(_currentPlayer == 'X' ? "Player" : "Bot");
                return true;
            }

            if (IsBoardFull())
            {
                UpdateGameStats("Draw");
                return true;
            }

            _currentPlayer = _currentPlayer == 'X' ? 'O' : 'X';

            if (_currentPlayer == 'O')
            {
                var move = _bot.GetNextMove(_board);

                // Перевірка координат на коректність ходу
                if (move.row >= 0 && move.row < 3 && move.col >= 0 && move.col < 3 && _board[move.row, move.col] == '\0')
                {
                    _board[move.row, move.col] = 'O';
                    if (CheckWinner())
                    {
                        UpdateGameStats("Bot");
                        return true;
                    }
                    if (IsBoardFull())
                    {
                        UpdateGameStats("Draw");
                        return true;
                    }
                    _currentPlayer = 'X';
                }
                else
                {
                    _currentPlayer = 'X';
                }
            }

            return false;
        }

        /// <summary>
        /// Перевіряє, чи повністю заповнене ігрове поле.
        /// </summary>
        /// <returns>True, якщо поле повністю заповнене, інакше False.</returns>
        public bool IsBoardFull()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (_board[row, col] == '\0')
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Повертає підказку для наступного ходу бота.
        /// </summary>
        /// <returns>Координати клітинки для наступного ходу.</returns>
        public (int row, int col) GetHint()
        {
            return _bot.GetNextMove(_board);
        }

        /// <summary>
        /// Перевіряє, чи є переможець в поточному стані ігрового поля.
        /// </summary>
        /// <returns>True, якщо є переможець, інакше false.</returns>
        public bool CheckWinner()
        {
            // Перевірка рядків, стовпців і діагоналей
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

        /// <summary>
        /// Оновлює статистику гравця і бота після завершення гри.
        /// </summary>
        /// <param name="winner">Переможець ("Player", "Bot" або "Draw").</param>
        private void UpdateGameStats(string winner)
        {
            var playerId = MainWindow.GetLoggedInPlayerId();
            var player = _playerRepository.GetPlayerById(playerId);

            if (player == null)
            {
                throw new ArgumentException((string)Application.Current.FindResource("StringPlayerNotFoundError"));
            }

            if (winner == "Player")
            {
                player.Wins++;
            }
            else if (winner == "Bot")
            {
                player.Losses++;
            }
            else if (winner == "Draw")
            {
                player.Draws++;
            }

            player.GamesPlayed++;
            _playerRepository.UpdatePlayer(player);
        }

        /// <summary>
        /// Повертає поточний стан ігрового поля.
        /// </summary>
        /// <returns>Масив символів, що представляє ігрове поле.</returns>
        public char[,] GetBoard() => _board;
    }
}