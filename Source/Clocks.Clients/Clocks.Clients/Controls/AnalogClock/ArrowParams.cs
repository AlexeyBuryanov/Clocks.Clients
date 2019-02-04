namespace Clocks.Clients.Core.Controls.AnalogClock
{
    /// <summary>
    /// Структура для хранения информации о трёх стрелках
    /// </summary>
    public struct ArrowParams
    {
        public ArrowParams(double width, double height, double offset)
        {
            Width = width;
            Height = height;
            Offset = offset;
        }

        /// <summary>
        /// Доля радиуса
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// Высота
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// Смещение относительно центральной оси
        /// </summary>
        public double Offset { get; set; }
    }
}