using System.Windows.Controls;

namespace TicTacToe.Views.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для CustomPasswordBox.xaml
    /// </summary>
    public partial class CustomPasswordBox : UserControl
    {
        public CustomPasswordBox()
        {
            InitializeComponent();
        }

        public string Password
        {
            get { return PART_PasswordBox.Password; }
            set { PART_PasswordBox.Password = value; }
        }

        public void SetError(string errorMessage)
        {
            PART_ErrorText.Text = errorMessage;
            PART_ErrorText.Visibility = string.IsNullOrEmpty(errorMessage) ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }
    }
}