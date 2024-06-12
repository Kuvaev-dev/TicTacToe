using System;
using System.Collections.Generic;

namespace TicTacToe.Bots
{
    public class SimpleBot : IBot
    {
        private Random _random = new Random();

        public (int row, int col) GetNextMove(char[,] board)
        {
            List<(int row, int col)> availableMoves = new List<(int row, int col)>();

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0')
                    {
                        availableMoves.Add((row, col));
                    }
                }
            }

            if (availableMoves.Count > 0)
            {
                int index = _random.Next(availableMoves.Count);
                return availableMoves[index];
            }

            return (-1, -1); // No available moves
        }
    }
}
