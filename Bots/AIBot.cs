using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Bots
{
    public class AIBot : IBot
    {
        private const char Player = 'X';
        private const char Bot = 'O';

        public (int row, int col) GetNextMove(char[,] board)
        {
            var bestMove = Minimax(board, Bot, 0);
            return bestMove.Move;
        }

        private (int Score, (int row, int col) Move) Minimax(char[,] board, char currentPlayer, int depth)
        {
            if (CheckWinner(board, Player))
                return (-10 + depth, (-1, -1)); // Player wins, penalize deeper moves less
            if (CheckWinner(board, Bot))
                return (10 - depth, (-1, -1)); // Bot wins, reward quicker wins
            if (IsBoardFull(board))
                return (0, (-1, -1)); // Draw

            var moves = new List<(int Score, (int row, int col) Move)>();

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0')
                    {
                        board[row, col] = currentPlayer;
                        int score = Minimax(board, currentPlayer == Bot ? Player : Bot, depth + 1).Score;
                        moves.Add((score, (row, col)));
                        board[row, col] = '\0'; // Undo move
                    }
                }
            }

            if (currentPlayer == Bot)
            {
                return moves.OrderByDescending(m => m.Score).First();
            }
            else
            {
                return moves.OrderBy(m => m.Score).First();
            }
        }

        private bool CheckWinner(char[,] board, char player)
        {
            // Check rows, columns and diagonals for a winning move
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) return true;
                if (board[0, i] == player && board[1, i] == player && board[2, i] == player) return true;
            }
            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) return true;
            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player) return true;
            return false;
        }

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
