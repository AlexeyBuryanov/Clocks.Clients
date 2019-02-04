using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clocks.Clients.Core.Models.Database
{
    /// <summary>
    /// Тип часов
    /// </summary>
    public class ClockType
    {
        [Key]
        public int ClockTypeId { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        public ICollection<Clock> Clocks { get; set; }
    }
}