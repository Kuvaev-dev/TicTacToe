using System;

namespace TicTacToe.Strategies
{
    public class BotStrategyHard : IBotStrategy
    {
        public (int, int) GetNextMove(char[,] board)
        {
            int bestScore = int.MinValue;
            (int, int) bestMove = (-1, -1);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        board[i, j] = 'O';
                        int score = Minimax(board, 0, false);
                        board[i, j] = ' ';
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = (i, j);
                        }
                    }
                }
            }
            return bestMove;
        }

        private int Minimax(char[,] board, int depth, bool isMaximizing)
        {
            char result = CheckWinner(board);
            if (result != ' ')
            {
                return result == 'O' ? 10 - depth : depth - 10;
            }

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == ' ')
                        {
                            board[i, j] = 'O';
                            int score = Minimax(board, depth + 1, false);
                            board[i, j] = ' ';
                            bestScore = Math.Max(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == ' ')
                        {
                            board[i, j] = 'X';
                            int score = Minimax(board, depth + 1, true);
                            board[i, j] = ' ';
                            bestScore = Math.Min(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
        }

        private char CheckWinner(char[,] board)
        {
            // Check rows, columns, and diagonals for a win
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != ' ')
                    return board[i, 0];
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != ' ')
                    return board[0, i];
            }
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != ' ')
                return board[0, 0];
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != ' ')
                return board[0, 2];
            return ' '; // No winner yet
        }
    }
}
