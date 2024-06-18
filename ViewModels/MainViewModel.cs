using System.Windows.Controls;
using TicTacToe.ViewModels.Base;
using TicTacToe.Views;

namespace TicTacToe.ViewModels
{
    /// <summary>
    /// ViewModel, що управляє головним вікном додатка.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Page _currentPage;

        /// <summary>
        /// Поточна сторінка в головному вікні додатка.
        /// </summary>
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Створює екземпляр MainViewModel зі стартовою сторінкою входу.
        /// </summary>
        public MainViewModel()
        {
            // Встановлюємо початкову сторінку входу
            CurrentPage = new LoginPage(this);
        }

        /// <summary>
        /// Навігація на вказану сторінку.
        /// </summary>
        /// <param name="page">Сторінка для навігації.</param>
        public void NavigateTo(Page page)
        {
            CurrentPage = page;
        }
    }
}