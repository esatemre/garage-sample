using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGarage.Sample.Core.Entities
{
    public class Garage : Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int GarageOwnerId { get; set; }

        /// <summary>
        /// true: online, false: offline
        /// </summary>
        [Required]
        public bool Status { get; set; }

        public virtual GarageOwner GarageOwner { get; set; }

        public virtual List<GarageDoor> GarageDoors { get; set; }
    }
}
