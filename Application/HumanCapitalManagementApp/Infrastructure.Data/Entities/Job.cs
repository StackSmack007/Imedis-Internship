using FluentNHibernate.Mapping;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class Job : BaseEntity<int>
    {
        public Job()
        {
            UserJobs = new List<UserJob>();
        }
        public virtual string Title { get; set; }
        public virtual int MinimumEducation { get; set; }
        public virtual Department Department { get; set; }
        public virtual IList<UserJob> UserJobs { get; set; }
    }

    public class JobMap : ClassMap<Job>
    {
        public JobMap()
        {
            Id(x => x.Id)
                 .GeneratedBy.TriggerIdentity();
            Map(x => x.Title);
            Map(x => x.MinimumEducation, "min_education_lvl");
            HasMany(x => x.UserJobs).Inverse().Cascade.All();
            References(x => x.Department, "department_id").Cascade.None();
            Table("Jobs");
        }
    }

}
