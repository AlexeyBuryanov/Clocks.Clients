using System;
using System.Linq;
using Xamarin.Forms;

namespace Clocks.Clients.Core.Controls.AnalogClock
{
    /// <inheritdoc />
    /// <summary>
    /// Аналоговые часы. Элемент управления базируется на BoxView.
    /// </summary>
    public class ClockView : ContentView
    {
        private readonly AbsoluteLayout _absoluteLayout = new AbsoluteLayout();
        private readonly BoxView _secondArrow = new BoxView();
        private readonly BoxView _minuteArrow = new BoxView();
        private readonly BoxView _hourArrow = new BoxView();
        private readonly BoxView[] _tickMarks = new BoxView[60];
        private readonly ArrowParams _secondParams = new ArrowParams(0.02, 1.1, 0.85);
        private readonly ArrowParams _minuteParams = new ArrowParams(0.05, 0.8, 0.9);
        private readonly ArrowParams _hourParams = new ArrowParams(0.125, 0.65, 0.9);

        /// <summary>
        /// Смещение по дефолту (UTC)
        /// </summary>
        private double _offset = 0;

        public ClockView()
        {
            _hourArrow.Color = Color.Black;
            _minuteArrow.Color = Color.Gray;
            _secondArrow.Color = Color.DarkRed;

            _absoluteLayout.Children.Add(_secondArrow);
            _absoluteLayout.Children.Add(_minuteArrow);
            _absoluteLayout.Children.Add(_hourArrow);

            // Создайте отметки (для определения размера и позиционирования позже)
            for (var i = 0; i < _tickMarks.Length; i++)
            {
                _tickMarks[i] = new BoxView { Color = Color.Black };
                _absoluteLayout.Children.Add(_tickMarks[i]);
            }

            Device.StartTimer(TimeSpan.FromSeconds(1.0 / 60), OnTimerTick);

            _absoluteLayout.LayoutChanged += OnAbsoluteLayoutSizeChanged;
            Content = _absoluteLayout;
        }

        #region Bindable Properties

        #region HourArrowColorProperty

        public static readonly BindableProperty HourArrowColorProperty =
            BindableProperty.Create(
                nameof(HourArrowColor),
                typeof(Color),
                typeof(ClockView),
                Color.FromHex("#000000"),
                propertyChanged: (bindable, oldValue, newValue) =>
                    (bindable as ClockView)?.ChangeHourArrowColor((Color)newValue, (Color)oldValue));
        public Color HourArrowColor {
            get => (Color)GetValue(HourArrowColorProperty);
            set => SetValue(HourArrowColorProperty, value);
        }
        private void ChangeHourArrowColor(Color newValue, Color oldValue)
        {
            if (newValue == oldValue) return;
            _hourArrow.Color = newValue;
        }

        #endregion

        #region MinuteArrowColorProperty

        public static readonly BindableProperty MinuteArrowColorProperty =
            BindableProperty.Create(
                nameof(MinuteArrowColor),
                typeof(Color),
                typeof(ClockView),
                Color.FromHex("#000000"),
                propertyChanged: (bindable, oldValue, newValue) =>
                    (bindable as ClockView)?.ChangeMinuteArrowColor((Color)newValue, (Color)oldValue));
        public Color MinuteArrowColor {
            get => (Color)GetValue(MinuteArrowColorProperty);
            set => SetValue(MinuteArrowColorProperty, value);
        }
        private void ChangeMinuteArrowColor(Color newValue, Color oldValue)
        {
            if (newValue == oldValue) return;
            _minuteArrow.Color = newValue;
        }

        #endregion

        #region SecondArrowColorProperty

        public static readonly BindableProperty SecondArrowColorProperty =
            BindableProperty.Create(
                nameof(SecondArrowColor),
                typeof(Color),
                typeof(ClockView),
                Color.FromHex("#000000"),
                propertyChanged: (bindable, oldValue, newValue) =>
                    (bindable as ClockView)?.ChangeSecondArrowColor((Color)newValue, (Color)oldValue));
        public Color SecondArrowColor {
            get => (Color)GetValue(SecondArrowColorProperty);
            set => SetValue(SecondArrowColorProperty, value);
        }
        private void ChangeSecondArrowColor(Color newValue, Color oldValue)
        {
            if (newValue == oldValue) return;
            _secondArrow.Color = newValue;
        }

        #endregion

        #region TickMarksColorProperty

        public static readonly BindableProperty TickMarksColorProperty =
            BindableProperty.Create(
                nameof(TickMarksColor),
                typeof(Color),
                typeof(ClockView),
                Color.FromHex("#000000"),
                propertyChanged: (bindable, oldValue, newValue) =>
                    (bindable as ClockView)?.ChangeTickMarksColor((Color)newValue, (Color)oldValue));
        public Color TickMarksColor {
            get => (Color)GetValue(TickMarksColorProperty);
            set => SetValue(TickMarksColorProperty, value);
        }
        private void ChangeTickMarksColor(Color newValue, Color oldValue)
        {
            if (newValue == oldValue) return;
            _tickMarks.ToList().ForEach(box =>
            {
                box.Color = newValue;
            });
        }

        #endregion

        #region OffsetTimeProperty

        public static readonly BindableProperty OffsetTimeProperty =
            BindableProperty.Create(
                nameof(OffsetTime),
                typeof(double),
                typeof(ClockView),
                1.0,
                propertyChanged: (bindable, oldValue, newValue) =>
                    (bindable as ClockView)?.ChangeOffsetTime((double)newValue, (double)oldValue));
        public double OffsetTime {
            get => (double)GetValue(OffsetTimeProperty);
            set => SetValue(OffsetTimeProperty, value);
        }
        private void ChangeOffsetTime(double newValue, double oldValue)
        {
            if (newValue == oldValue) return;
            _offset = newValue;
        }

        #endregion

        #endregion

        private void OnAbsoluteLayoutSizeChanged(object sender, EventArgs args)
        {
            // Получаем центр и радиус AbsoluteLayout
            var center = new Point(_absoluteLayout.Width / 2, _absoluteLayout.Height / 2);
            var radius = 0.45 * Math.Min(_absoluteLayout.Width, _absoluteLayout.Height);

            // Положение, размер и вращение 60 отметок
            for (var index = 0; index < _tickMarks.Length; index++)
            {
                var size = radius / (index % 5 == 0 ? 15 : 30);
                var radians = index * 2 * Math.PI / _tickMarks.Length;
                var x = center.X + radius * Math.Sin(radians) - size / 2;
                var y = center.Y - radius * Math.Cos(radians) - size / 2;
                AbsoluteLayout.SetLayoutBounds(_tickMarks[index], new Rectangle(x, y, size, size));
                _tickMarks[index].Rotation = 180 * radians / Math.PI;
            }

            // Положение и размер трех стрелок
            LayoutArrow(_secondArrow, _secondParams, center, radius);
            LayoutArrow(_minuteArrow, _minuteParams, center, radius);
            LayoutArrow(_hourArrow, _hourParams, center, radius);
        }

        private static void LayoutArrow(VisualElement boxView, ArrowParams arrowParams, Point center, double radius)
        {
            var width = arrowParams.Width * radius;
            var height = arrowParams.Height * radius;
            var offset = arrowParams.Offset;

            AbsoluteLayout.SetLayoutBounds(boxView,
                new Rectangle(center.X - 0.5 * width,
                              center.Y - offset * height,
                              width, height));

            // Установка свойства AnchorY для вращений
            boxView.AnchorY = arrowParams.Offset;
        }

        private bool OnTimerTick()
        {
            // Получаем текущее время по UTC учитывая смещение (часовой пояс / таймзону)
            var dateTime = DateTime.UtcNow.AddHours(_offset);

            // Установка угла поворота для часовой и минутной стрелок
            _hourArrow.Rotation = 30 * (dateTime.Hour % 12) + 0.5 * dateTime.Minute;
            _minuteArrow.Rotation = 6 * dateTime.Minute + 0.1 * dateTime.Second;

            // Анимация для секундной стрелки
            var t = dateTime.Millisecond / 1000.0;

            if (t < 0.5)
            {
                t = 0.5 * Easing.SpringIn.Ease(t / 0.5);
            }
            else
            {
                t = 0.5 * (1 + Easing.SpringOut.Ease((t - 0.5) / 0.5));
            }

            _secondArrow.Rotation = 6 * (dateTime.Second + t);

            return true;
        }
    }
}
