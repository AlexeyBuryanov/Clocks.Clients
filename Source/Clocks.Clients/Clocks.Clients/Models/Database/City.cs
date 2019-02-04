using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clocks.Clients.Core.Models.Database
{
    /// <summary>
    /// Страна
    /// </summary>
    public class City
    {
        [Key]
        public int CityId { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Смещение времени относительно Coordinated Universal Time (UTC)
        /// </summary>
        public double Offset { get; set; }

        public ICollection<Clock> Clocks { get; set; }
    }
}
