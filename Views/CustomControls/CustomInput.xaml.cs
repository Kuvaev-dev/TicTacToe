using System.Windows;
using System.Windows.Controls;

namespace TicTacToe.Views.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для CustomInput.xaml
    /// </summary>
    public partial class CustomInput : TextBox
    {
        public CustomInput()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CustomInput), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }

    public class CustomValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult(false, "Поле не может быть пустым");
            }
            return ValidationResult.ValidResult;
        }
    }
}
