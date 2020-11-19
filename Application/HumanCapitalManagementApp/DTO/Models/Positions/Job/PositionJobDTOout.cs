using Helpers.Interfaces;
using Infrastructure.Data;

namespace DTO
{
    public class PositionJobDTOout : IMapFrom<Job>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public EducationGrade MinimumEducation { get; set; }
        //public int DepartmentId { get; set; }
        //public string DepartmentTitle { get; set; }
    }
}
