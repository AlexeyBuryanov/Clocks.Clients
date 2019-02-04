using Amporis.Xamarin.Forms.ColorPicker;
using Prism.Mvvm;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xamarin.Forms;

namespace Clocks.Clients.Core.Models.Database
{
    /// <summary>
    /// Часы, модель для БД
    /// </summary>
    public class Clock : BindableBase
    {
        [Key]
        public int ClockId { get; set; }

        [ForeignKey("CityId")]
        public int CityId { get; set; }
        public City City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        [ForeignKey("ClockTypeId")]
        public int ClockTypeId { get; set; }
        public ClockType ClockType { get; set; }

        public string HourArrowColorHex { get; set; }
        public string MinuteArrowColorHex { get; set; }
        public string SecondArrowColorHex { get; set; }
        public string TickMarksColorHex { get; set; }


        /* ******************** Not Mapped ********************* */
        [NotMapped]
        private City _city;
        [NotMapped]
        private string _description;
        [NotMapped]
        private string _hourArrowColorHex;
        [NotMapped]
        private string _minuteArrowColorHex;
        [NotMapped]
        private string _secondArrowColorHex;
        [NotMapped]
        private string _tickMarksColorHex;

        [NotMapped]
        private Color _hourArrowColor;
        [NotMapped]
        private Color _minuteArrowColor;
        [NotMapped]
        private Color _secondArrowColor;
        [NotMapped]
        private Color _tickMarksColor;

        /// <summary>
        /// Описание часов (в моём случае я вывожу в это поле просто время в цифрах)
        /// </summary>
        [NotMapped]
        public string Description {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        [NotMapped]
        public Color HourArrowColor {
            get => Color.FromHex(HourArrowColorHex);
            set {
                HourArrowColorHex = value.ToHex();
                _hourArrowColorHex = HourArrowColorHex;
                SetProperty(ref _hourArrowColorHex, value.ToHex());
                SetProperty(ref _hourArrowColor, value);
            }
        }
        [NotMapped]
        public Color MinuteArrowColor {
            get => Color.FromHex(MinuteArrowColorHex);
            set {
                MinuteArrowColorHex = value.ToHex();
                _minuteArrowColorHex = MinuteArrowColorHex;
                SetProperty(ref _minuteArrowColorHex, value.ToHex());
                SetProperty(ref _minuteArrowColor, value);
            }
        }
        [NotMapped]
        public Color SecondArrowColor {
            get => Color.FromHex(SecondArrowColorHex);
            set {
                SecondArrowColorHex = value.ToHex();
                _secondArrowColorHex = SecondArrowColorHex;
                SetProperty(ref _secondArrowColorHex, value.ToHex());
                SetProperty(ref _secondArrowColor, value);
            }
        }
        [NotMapped]
        public Color TickMarksColor {
            get => Color.FromHex(TickMarksColorHex);
            set {
                TickMarksColorHex = value.ToHex();
                _tickMarksColorHex = TickMarksColorHex;
                SetProperty(ref _tickMarksColorHex, value.ToHex());
                SetProperty(ref _tickMarksColor, value);
            }
        }
    }
}