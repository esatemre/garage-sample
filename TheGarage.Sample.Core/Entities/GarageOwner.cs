using System.ComponentModel.DataAnnotations;

namespace TheGarage.Sample.Core.Entities
{
    public class GarageOwner : Entity
    {

        [Required]
        public string FullName { get; set; }

        public virtual Garage Garage { get; set; }
    }
}
