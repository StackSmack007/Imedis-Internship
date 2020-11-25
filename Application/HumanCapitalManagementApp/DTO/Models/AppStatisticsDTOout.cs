using System.Collections.Generic;

namespace DTO
{
    public class AppStatisticsDTOout
    {
        public ICollection<string> Users { get; set; } = new List<string>();
        public ICollection<string> Companies { get; set; } = new List<string>();
        public int PositionsActive { get; set; }
        public int PositionsClosed { get; set; }
    }
}
