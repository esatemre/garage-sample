using System.Collections.Generic;

namespace TheGarage.Sample.Core.Services.DTOs
{
    public class GarageDoorDto
    {
        public string IpAddress { get; set; }
        /// <summary>
        /// true: online, false: offline
        /// </summary>
        public bool Status { get; set; }

        public List<GarageDoorUpdateDto> Updates { get; set; }
    }
}
