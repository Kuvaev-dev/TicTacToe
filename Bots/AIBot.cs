using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Bots
{
    /// <summary>
    /// Представляє ІІ бота для гри в хрестики-нулики, що використовує алгоритм Minimax.
    /// </summary>
    public class AIBot : IBot
    {
        private const char Player = 'X';
        private const char Bot = 'O';

        /// <summary>
        /// Отримує наступний хід для бота, використовуючи алгоритм Minimax.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <returns>Кортеж, що містить номер рядка та стовпця наступного ходу.</returns>
        public (int row, int col) GetNextMove(char[,] board)
        {
            var bestMove = Minimax(board, Bot, 0);
            return bestMove.Move;
        }

        /// <summary>
        /// Алгоритм Minimax для визначення найкращого ходу для поточного гравця.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <param name="currentPlayer">Поточний гравець ('X' або 'O').</param>
        /// <param name="depth">Глибина рекурсії.</param>
        /// <returns>Кортеж, що містить оцінку стану поля та номер рядка і стовпця найкращого ходу.</returns>
        private (int Score, (int row, int col) Move) Minimax(char[,] board, char currentPlayer, int depth)
        {
            // Перевірка на переможця
            if (CheckWinner(board, Player))
                return (-10 + depth, (-1, -1)); // Гравець виграє, штраф за більш глибокі ходи
            if (CheckWinner(board, Bot))
                return (10 - depth, (-1, -1)); // Бот виграє, нагорода за швидкіші перемоги
            if (IsBoardFull(board))
                return (0, (-1, -1)); // Нічия

            var moves = new List<(int Score, (int row, int col) Move)>();

            // Проходження по всіх клітинках для пошуку доступних ходів
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0')
                    {
                        // Виконання ходу
                        board[row, col] = currentPlayer;
                        int score = Minimax(board, currentPlayer == Bot ? Player : Bot, depth + 1).Score;
                        moves.Add((score, (row, col)));
                        // Скасування ходу
                        board[row, col] = '\0';
                    }
                }
            }

            // Повернення найкращого ходу для поточного гравця
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
        /// Перевіряє, чи є поточний гравець переможцем.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <param name="player">Поточний гравець ('X' або 'O').</param>
        /// <returns>True, якщо гравець виграв, інакше False.</returns>
        private bool CheckWinner(char[,] board, char player)
        {
            // Перевірка рядків, стовпців і діагоналей на переможний хід
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
        /// Перевіряє, чи повністю заповнене ігрове поле.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <returns>True, якщо поле повністю заповнене, інакше False.</returns>
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
