using System;
using System.ComponentModel.DataAnnotations;

namespace TheGarage.Sample.Core.Entities
{
    public class GarageDoorUpdate : Entity
    {
        [Required]
        public int GarageDoorId { get; set; }

        /// <summary>
        /// true: online, false: offline
        /// </summary>
        public bool PreviousStatus { get; set; }

        [Required]
        public DateTime UpdateTime { get; set; }

        public virtual GarageDoor GarageDoor { get; set; }
    }
}
