using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TicTacToe.ViewModels.Base
{
    /// <summary>
    /// Базовий клас для ViewModel з реалізацією INotifyPropertyChanged.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Подія, що виникає при зміні властивостей ViewModel.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод для сповіщення про зміну властивостей.
        /// </summary>
        /// <param name="propertyName">Назва властивості, що змінюється.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}