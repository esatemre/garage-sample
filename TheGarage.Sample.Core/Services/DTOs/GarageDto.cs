using System.Collections.Generic;

namespace TheGarage.Sample.Core.Services.DTOs
{
    public class GarageDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public List<GarageDoorDto> Doors { get; set; }
    }
}
