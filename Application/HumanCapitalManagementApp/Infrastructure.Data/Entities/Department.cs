using FluentNHibernate.Mapping;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class Department : BaseEntity<int>
    {
        public Department()
        {
            Jobs = new List<Job>();
        }
        
        public virtual string Title { get; set; }
        public virtual IList<Job> Jobs { get; set; }
    }

    public class DepartmentMap : ClassMap<Department>
    {
        public DepartmentMap()
        {
            Id(x => x.Id)
               .GeneratedBy.TriggerIdentity()
                ;
            Map(x => x.Title);
            HasMany(x => x.Jobs).Inverse().Cascade.All();
            Table("Departments");
        }
    }
}