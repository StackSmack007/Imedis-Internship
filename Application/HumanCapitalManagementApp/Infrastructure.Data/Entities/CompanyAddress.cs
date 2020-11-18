using FluentNHibernate.Mapping;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class CompanyAddress : BaseEntity<int>
    {
        public CompanyAddress()
        {
            UserJobs = new List<UserJob>();
        }
        public virtual string Address { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual bool IsPrimary { get; set; } = false;
        public virtual Town Town { get; set; }
        public virtual Company Company { get; set; }
        public virtual IList<UserJob> UserJobs { get; set; }
    }

    public class CompanyAddressMap : ClassMap<CompanyAddress>
    {
        public CompanyAddressMap()
        {
            Id(x =>x.Id)
                .GeneratedBy.TriggerIdentity()
                ;
            Map(x => x.Address);
            Map(x => x.Email);
            Map(x => x.Phone);
            Map(x => x.IsPrimary,"is_primary");
            References(x => x.Town,"town_id").Cascade.All();
            References(x => x.Company,"company_id").Cascade.All();
            HasMany(x => x.UserJobs).Inverse().Cascade.All();
            Table("CompanyAddresses");
        }
    }

}
