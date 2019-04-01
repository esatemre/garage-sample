using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGarage.Sample.Core.Entities
{
    public class GarageDoor : Entity
    {

        [Required]
        public int GarageId { get; set; }

        [Required]
        public string IpAddress { get; set; }

        /// <summary>
        /// true: online, false: offline
        /// </summary>
        [Required]
        public bool Status { get; set; }

        public virtual Garage Garage { get; set; }

        public virtual List<GarageDoorUpdate> GarageDoorUpdates { get; set; }
    }
}
