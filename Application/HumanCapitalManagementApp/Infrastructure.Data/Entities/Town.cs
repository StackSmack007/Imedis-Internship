using FluentNHibernate.Mapping;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class Town : BaseEntity<int>
    {
        public Town()
        {
            Users = new List<User>();
            CompanyAddresses = new List<CompanyAddress>();
        }
        
        public virtual string Name { get; set; }
        public virtual string PostalCode { get; set; }

        public virtual Country Country { get; set; }

        public virtual IList<User> Users { get; set; }
        public virtual IList<CompanyAddress> CompanyAddresses { get; set; }
    }
    public class TownMap : ClassMap<Town>
    {
        public TownMap()
        {
            Id(x => x.Id)
                .GeneratedBy.TriggerIdentity()
                ;
            Map(x => x.Name);
            Map(x => x.PostalCode, "postal_code").Nullable();
            HasMany(x => x.Users).Inverse().Cascade.All();
            HasMany(x => x.CompanyAddresses).Inverse().Cascade.All();
            References(x => x.Country,"country_id").Cascade.None();
            Table("Towns");
        }
    }
}
