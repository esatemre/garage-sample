using System;

namespace TheGarage.Sample.Core.Services.DTOs
{
    public class GarageDoorUpdateDto
    {
        /// <summary>
        /// true: online, false: offline
        /// </summary>
        public bool PreviousStatus { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
