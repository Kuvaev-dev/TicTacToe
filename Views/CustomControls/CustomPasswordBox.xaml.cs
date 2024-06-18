using System.Windows;
using System.Windows.Controls;

namespace TicTacToe.Views.CustomControls
{
    /// <summary>
    /// Логіка взаємодії для CustomPasswordBox.xaml
    /// </summary>
    public partial class CustomPasswordBox : UserControl
    {
        public CustomPasswordBox()
        {
            InitializeComponent();
            PART_PasswordBox.PasswordChanged += OnPasswordChanged;
        }

        /// <summary>
        /// Властивість для отримання або задання пароля.
        /// </summary>
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(CustomPasswordBox), new PropertyMetadata("", OnPasswordPropertyChanged));

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (CustomPasswordBox)d;
            if (control.PART_PasswordBox.Password != (string)e.NewValue)
            {
                control.PART_PasswordBox.Password = (string)e.NewValue;
            }
            control.UpdatePlaceholderVisibility();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = PART_PasswordBox.Password;
            UpdatePlaceholderVisibility();
        }

        private void UpdatePlaceholderVisibility()
        {
            var placeholder = PART_PasswordBox.Template.FindName("PART_Placeholder", PART_PasswordBox) as TextBlock;
            if (placeholder != null)
            {
                placeholder.Visibility = string.IsNullOrEmpty(PART_PasswordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Властивість для отримання або задання тексту-заповнювача для поля пароля.
        /// </summary>
        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register("PlaceholderText", typeof(string), typeof(CustomPasswordBox), new PropertyMetadata("Enter your password"));
    }
}