using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Bots
{
    /// <summary>
    /// Представляет ИИ бота для игры в крестики-нолики, использующего алгоритм Minimax.
    /// </summary>
    public class AIBot : IBot
    {
        private const char Player = 'X';
        private const char Bot = 'O';

        /// <summary>
        /// Получает следующий ход для бота, используя алгоритм Minimax.
        /// </summary>
        /// <param name="board">Текущее состояние игрового поля.</param>
        /// <returns>Кортеж, содержащий номер строки и столбца следующего хода.</returns>
        public (int row, int col) GetNextMove(char[,] board)
        {
            var bestMove = Minimax(board, Bot, 0);
            return bestMove.Move;
        }

        /// <summary>
        /// Алгоритм Minimax для определения наилучшего хода для текущего игрока.
        /// </summary>
        /// <param name="board">Текущее состояние игрового поля.</param>
        /// <param name="currentPlayer">Текущий игрок ('X' или 'O').</param>
        /// <param name="depth">Глубина рекурсии.</param>
        /// <returns>Кортеж, содержащий оценку состояния поля и номер строки и столбца наилучшего хода.</returns>
        private (int Score, (int row, int col) Move) Minimax(char[,] board, char currentPlayer, int depth)
        {
            // Проверка на победителя
            if (CheckWinner(board, Player))
                return (-10 + depth, (-1, -1)); // Игрок выигрывает, штраф за более глубокие ходы
            if (CheckWinner(board, Bot))
                return (10 - depth, (-1, -1)); // Бот выигрывает, награда за более быстрые выигрыши
            if (IsBoardFull(board))
                return (0, (-1, -1)); // Ничья

            var moves = new List<(int Score, (int row, int col) Move)>();

            // Проход по всем клеткам для поиска доступных ходов
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0')
                    {
                        // Выполнение хода
                        board[row, col] = currentPlayer;
                        int score = Minimax(board, currentPlayer == Bot ? Player : Bot, depth + 1).Score;
                        moves.Add((score, (row, col)));
                        // Отмена хода
                        board[row, col] = '\0';
                    }
                }
            }

            // Возвращение наилучшего хода для текущего игрока
            if (currentPlayer == Bot)
            {
                return moves.OrderByDescending(m => m.Score).First();
            }
            else
            {
                return moves.OrderBy(m => m.Score).First();
            }
        }

        /// <summary>
        /// Проверяет, является ли текущий игрок победителем.
        /// </summary>
        /// <param name="board">Текущее состояние игрового поля.</param>
        /// <param name="player">Текущий игрок ('X' или 'O').</param>
        /// <returns>True, если игрок победил, иначе False.</returns>
        private bool CheckWinner(char[,] board, char player)
        {
            // Проверка строк, столбцов и диагоналей на победный ход
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) return true;
                if (board[0, i] == player && board[1, i] == player && board[2, i] == player) return true;
            }
            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) return true;
            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player) return true;
            return false;
        }

        /// <summary>
        /// Проверяет, полностью ли заполнено игровое поле.
        /// </summary>
        /// <param name="board">Текущее состояние игрового поля.</param>
        /// <returns>True, если поле полностью заполнено, иначе False.</returns>
        private bool IsBoardFull(char[,] board)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0')
                        return false;
                }
            }
            return true;
        }
    }
}
