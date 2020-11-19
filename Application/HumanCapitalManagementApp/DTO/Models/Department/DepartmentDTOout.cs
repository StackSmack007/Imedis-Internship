using Helpers.Interfaces;
using Infrastructure.Data;
using System.Collections.Generic;

namespace DTO
{
    public class DepartmentDTOout : IMapFrom<Department>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<JobDTOout> Jobs { get; set; } = new List<JobDTOout>();
    }

    public class JobDTOout : IMapFrom<Job>
    {
        public int Id { get; set; }
        public virtual string Title { get; set; }
        public EducationGrade MinimumEducation { get; set; }
        public int UserJobsCount { get; set; }
    }

}