using System;

namespace TicTacToe.Bots
{
    /// <summary>
    /// Представляє ІІ бота для гри в хрестики-нулики, що використовує алгоритм Minimax з альфа-бета відсіканням.
    /// </summary>
    public class AIBot : BotBase
    {
        /// <summary>
        /// Алгоритм Minimax з альфа-бета відсіканням для визначення найкращого ходу для поточного гравця.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <param name="currentPlayer">Поточний гравець ('X' або 'O').</param>
        /// <param name="depth">Глибина рекурсії.</param>
        /// <param name="alpha">Найкраща оцінка для гравця 'X' (мінімізатора) на поточному рівні рекурсії або вище.</param>
        /// <param name="beta">Найкраща оцінка для гравця 'O' (максимізатора) на поточному рівні рекурсії або вище.</param>
        /// <returns>Кортеж, що містить оцінку стану поля та номер рядка і стовпця найкращого ходу.</returns>
        protected (int Score, (int row, int col) Move) Minimax(char[,] board, char currentPlayer, int depth, int alpha, int beta)
        {
            // Перевірка на переможця
            if (CheckWinner(board, Player))
                return (-10 + depth, (-1, -1)); // Гравець виграє, штраф за більш глибокі ходи
            if (CheckWinner(board, Bot))
                return (10 - depth, (-1, -1)); // Бот виграє, нагорода за швидкіші перемоги
            if (IsBoardFull(board))
                return (0, (-1, -1)); // Нічия

            var bestMove = (-1, -1);
            int bestScore = (currentPlayer == Bot) ? int.MinValue : int.MaxValue;

            // Перебір кожної клітинки на дошці
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0') // Перевірка, чи клітина порожня
                    {
                        board[row, col] = currentPlayer; // Встановлення поточного гравця на клітинку
                        int score = Minimax(board, currentPlayer == Bot ? Player : Bot, depth + 1, alpha, beta).Score; // Рекурсивний виклик для наступного гравця

                        board[row, col] = '\0'; // Скасування ходу (розгляд варіантів)

                        // Вибір найкращого ходу залежно від поточного гравця (максимізатор або мінімізатор)
                        if (currentPlayer == Bot)
                        {
                            if (score > bestScore)
                            {
                                bestScore = score;
                                bestMove = (row, col); // Збереження поточного найкращого ходу
                            }
                            alpha = Math.Max(alpha, bestScore); // Оновлення альфа значення для максимізатора
                        }
                        else
                        {
                            if (score < bestScore)
                            {
                                bestScore = score;
                                bestMove = (row, col); // Збереження поточного найкращого ходу
                            }
                            beta = Math.Min(beta, bestScore); // Оновлення бета значення для мінімізатора
                        }

                        // Відсічення гілок (алфа-бета відсікання)
                        if (beta <= alpha)
                        {
                            break; // Зупинка перебору, якщо гілка відсічена
                        }
                    }
                }
            }

            return (bestScore, bestMove); // Повернення найкращого результату і ходу
        }

        /// <summary>
        /// Отримує наступний хід для бота, використовуючи алгоритм Minimax.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <returns>Кортеж, що містить номер рядка та стовпця наступного ходу.</returns>
        public override (int row, int col) GetNextMove(char[,] board)
        {
            var bestMove = Minimax(board, Bot, 0, int.MinValue, int.MaxValue);
            return bestMove.Move;
        }
    }
}
