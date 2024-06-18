using System.Windows;

namespace TicTacToe.Views
{
    /// <summary>
    /// Логіка взаємодії для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Властивість, яка повертає або встановлює ідентифікатор ввійшовшого в систему гравця.
        /// </summary>
        public static int LoggedInPlayerId { get; private set; }

        /// <summary>
        /// Конструктор головного вікна.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Встановлює ідентифікатор ввійшовшого в систему гравця.
        /// </summary>
        /// <param name="playerId">Ідентифікатор гравця.</param>
        public static void SetLoggedInPlayerId(int playerId)
        {
            LoggedInPlayerId = playerId;
        }

        /// <summary>
        /// Повертає ідентифікатор ввійшовшого в систему гравця.
        /// </summary>
        /// <returns>Ідентифікатор гравця.</returns>
        public static int GetLoggedInPlayerId()
        {
            return LoggedInPlayerId;
        }
    }
}