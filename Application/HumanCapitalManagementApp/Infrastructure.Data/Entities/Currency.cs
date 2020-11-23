using FluentNHibernate.Mapping;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class Currency : BaseEntity<int>
    {
        public Currency()
        {
            UserJobs = new List<UserJob>();
        }
        public virtual string Name { get; set; }
        public virtual decimal Rate { get; set; }
        public virtual IList<UserJob> UserJobs { get; set; }
    }

    public class CurrencyMap : ClassMap<Currency>
    {
        public CurrencyMap()
        {
            Id(x => x.Id)
               .GeneratedBy.TriggerIdentity()
                ;
            Map(x => x.Name);
            Map(x => x.Rate);
            HasMany(x=>x.UserJobs).Inverse().Cascade.All();
            Table("Currencies");
        }
    }

}
