namespace TicTacToe.Strategies
{
    public interface IBotStrategy
    {
        (int, int) GetNextMove(char[,] board);
    }
}