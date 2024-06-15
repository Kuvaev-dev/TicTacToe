using System.Windows;
using TicTacToe.ViewModels;

namespace TicTacToe.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Возвращает или устанавливает идентификатор вошедшего в систему игрока.
        /// </summary>
        public static int LoggedInPlayerId { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Устанавливает идентификатор вошедшего в систему игрока.
        /// </summary>
        /// <param name="playerId">Идентификатор игрока.</param>
        public static void SetLoggedInPlayerId(int playerId)
        {
            LoggedInPlayerId = playerId;
        }

        /// <summary>
        /// Возвращает идентификатор вошедшего в систему игрока.
        /// </summary>
        /// <returns>Идентификатор игрока.</returns>
        public static int GetLoggedInPlayerId()
        {
            return LoggedInPlayerId;
        }
    }
}
