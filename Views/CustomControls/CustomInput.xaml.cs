using System.Windows;
using System.Windows.Controls;

namespace TicTacToe.Views.CustomControls
{
    /// <summary>
    /// Логіка взаємодії для CustomInput.xaml
    /// </summary>
    public partial class CustomInput : TextBox
    {
        public CustomInput()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Залежність властивості Text, яка представляє текст, введений в текстовому полі.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CustomInput), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Отримує або задає текст, що введений в текстовому полі.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Залежність властивості Placeholder, яка представляє текст-заповнювач для текстового поля.
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(CustomInput), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Отримує або задає текст-заповнювач для текстового поля.
        /// </summary>
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
    }
}
