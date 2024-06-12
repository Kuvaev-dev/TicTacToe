using System.Windows.Navigation;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public static int LoggedInPlayerId { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public static void SetLoggedInPlayerId(int playerId)
        {
            LoggedInPlayerId = playerId;
        }

        public static int GetLoggedInPlayerId()
        {
            return LoggedInPlayerId;
        }
    }
}
