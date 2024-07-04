using System;
using System.Collections.Generic;

namespace TicTacToe.Bots
{
    /// <summary>
    /// Представляє простого бота, який робить випадкові ходи.
    /// </summary>
    public class SimpleBot : IBot
    {
        private static readonly Random Random = new Random();

        /// <summary>
        /// Отримує випадковий наступний хід для бота.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <returns>Кортеж, що містить номер рядка та стовпця наступного ходу.</returns>
        public (int row, int col) GetNextMove(char[,] board)
        {
            List<(int row, int col)> emptyCells = new List<(int row, int col)>();

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0')
                    {
                        emptyCells.Add((row, col));
                    }
                }
            }

            if (emptyCells.Count > 0)
            {
                int index = Random.Next(emptyCells.Count);
                return emptyCells[index];
            }

            return (-1, -1); // Якщо немає доступних ходів
        }
    }
}