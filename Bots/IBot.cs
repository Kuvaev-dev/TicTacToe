namespace TicTacToe.Bots
{
    public interface IBot
    {
        (int row, int col) GetNextMove(char[,] board);
    }
}
