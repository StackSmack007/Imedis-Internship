using FluentNHibernate.Mapping;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class Role : BaseEntity<int>
    {
        public Role()
        {
            Users = new List<User>();
        }
        public virtual string Name { get; set; }
        public virtual IList<User> Users { get; set; }
    }

    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(x => x.Id)
               .GeneratedBy.TriggerIdentity()
                ;
            HasManyToMany(x=>x.Users).Cascade.All().Table("UserRoles");
            Map(x => x.Name);
            Table("Roles");
        }
    }
}
