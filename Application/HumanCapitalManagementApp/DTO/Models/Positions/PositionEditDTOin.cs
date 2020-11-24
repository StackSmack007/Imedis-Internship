using System;

namespace DTO
{
    public class PositionEditDTOin : PositionDTOin
    {
        public int Id { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
