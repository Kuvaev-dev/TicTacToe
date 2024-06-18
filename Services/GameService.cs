using System;
using TicTacToe.Bots;
using TicTacToe.Repositories;
using TicTacToe.Views;

namespace TicTacToe.Services
{
    /// <summary>
    /// Сервис для управления игровым процессом и взаимодействия с игроками и ботами.
    /// </summary>
    public class GameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private char[,] _board;
        private char _currentPlayer;
        private IBot _bot;

        /// <summary>
        /// Инициализирует новый экземпляр класса GameService с указанными репозиториями.
        /// </summary>
        /// <param name="gameRepository">Репозиторий для работы с данными об играх.</param>
        /// <param name="playerRepository">Репозиторий для работы с данными об игроках.</param>
        public GameService(IGameRepository gameRepository, IPlayerRepository playerRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _board = new char[3, 3];
            _currentPlayer = 'X'; // 'X' для игрока, 'O' для бота
        }

        /// <summary>
        /// Начинает новую игру с заданным уровнем сложности бота.
        /// </summary>
        /// <param name="botLevel">Уровень сложности бота.</param>
        public void StartNewGame(string botLevel)
        {
            _board = new char[3, 3];
            _currentPlayer = new Random().Next(0, 2) == 0 ? 'X' : 'O';

            _bot = botLevel switch
            {
                "НОВИЧОК" => new SimpleBot(),
                "ЗАЩИТА" => new DefensiveBot(),
                "НАПАДЕНИЕ" => new OffensiveBot(),
                "ГУРУ" => new GuruBot(),
                "ИИ" => new AIBot(),
                _ => new SimpleBot(),
            };
        }

        /// <summary>
        /// Возвращает текущего игрока (X - игрок, O - бот).
        /// </summary>
        public char GetCurrentPlayer() => _currentPlayer;

        /// <summary>
        /// Выполняет ход в указанную ячейку игрового поля.
        /// </summary>
        /// <param name="row">Номер строки.</param>
        /// <param name="col">Номер столбца.</param>
        /// <returns>True, если ход выполнен успешно, иначе false.</returns>
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
                        UpdateGameStats("Bot");
                        return true;
                    }
                    _currentPlayer = 'X';
                }
            }
            return false;
        }

        /// <summary>
        /// Возвращает подсказку для следующего хода бота.
        /// </summary>
        /// <returns>Координаты ячейки для следующего хода.</returns>
        public (int row, int col) GetHint()
        {
            return _bot.GetNextMove(_board);
        }

        /// <summary>
        /// Проверяет, есть ли победитель в текущем состоянии игрового поля.
        /// </summary>
        /// <returns>True, если есть победитель, иначе false.</returns>
        public bool CheckWinner()
        {
            // Проверка строк, столбцов и диагоналей
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
        /// Обновляет статистику игрока и бота после завершения игры.
        /// </summary>
        /// <param name="winner">Победитель ("Player" или "Bot").</param>
        private void UpdateGameStats(string winner)
        {
            var playerId = MainWindow.GetLoggedInPlayerId();
            var player = _playerRepository.GetPlayerById(playerId);

            if (player == null)
            {
                throw new ArgumentException("Player not found.");
            }

            if (winner == "Player")
            {
                player.Wins++;
            }
            else if (winner == "Bot")
            {
                player.Losses++;
            }
            else
            {
                player.Draws++;
            }

            player.GamesPlayed++;
            _playerRepository.UpdatePlayer(player);
        }

        /// <summary>
        /// Возвращает текущее состояние игрового поля.
        /// </summary>
        /// <returns>Массив символов, представляющий игровое поле.</returns>
        public char[,] GetBoard() => _board;
    }
}
