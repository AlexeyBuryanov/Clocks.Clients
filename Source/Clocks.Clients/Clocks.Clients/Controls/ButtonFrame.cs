using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Clocks.Clients.Core.Controls
{
    /// <inheritdoc />
    /// <summary>
    /// Фрейм для кнопки
    /// </summary>
    public class ButtonFrame : Frame
    {
        // Включаем тень
        public ButtonFrame() => HasShadow = true;

        /// <inheritdoc />
        /// <summary>
        /// При изменении св-ва. Переопределённый метод Frame
        /// </summary>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            // Если имя св-ва совпадает с именем свойства контента
            if (propertyName == ContentProperty.PropertyName)
            {
                ContentUpdated();
            }
        }

        /// <summary>
        /// Контент обновился
        /// </summary>
        private void ContentUpdated()
        {
            BackgroundColor = Content.BackgroundColor;
        }
    }
}
