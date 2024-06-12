using System;
using TicTacToe.Bots;
using TicTacToe.Repositories;

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
                        UpdateGameStats(_currentPlayer == 'X' ? "Player" : "Bot");
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
            // Обновление статистики игрока и бота в базе данных
            if (winner == "Player")
            {
                var player = _playerRepository.GetPlayerById(1); // Пример ID игрока
                player.Wins++;
                player.GamesPlayed++;
                _playerRepository.UpdatePlayer(player);
            }
            else if (winner == "Bot")
            {
                var botPlayer = _playerRepository.GetPlayerById(2); // Пример ID бота
                botPlayer.Wins++;
                botPlayer.GamesPlayed++;
                _playerRepository.UpdatePlayer(botPlayer);
            }
        }

        /// <summary>
        /// Возвращает текущее состояние игрового поля.
        /// </summary>
        /// <returns>Массив символов, представляющий игровое поле.</returns>
        public char[,] GetBoard() => _board;
    }
}