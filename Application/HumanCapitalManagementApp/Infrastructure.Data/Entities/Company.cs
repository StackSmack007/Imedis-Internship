using FluentNHibernate.Mapping;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class Company : BaseEntity<string>
    {
        public Company()
        {
            CompanyAddresses = new List<CompanyAddress>();
            Id = System.Guid.NewGuid().ToString();
        }
        public virtual string Name { get; set; }
        public virtual string WebPage { get; set; }
        public virtual IList<CompanyAddress> CompanyAddresses { get; set; }
    }

    public class CompanyMap : ClassMap<Company>
    {
        public CompanyMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.WebPage, "web_page");
            HasMany(x => x.CompanyAddresses).Inverse().Cascade.All();
            Table("Companies");
        }
    }

}
