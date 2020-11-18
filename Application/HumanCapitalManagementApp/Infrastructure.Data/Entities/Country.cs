using FluentNHibernate.Mapping;
using System.Collections.Generic;
namespace Infrastructure.Data
{
    public class Country : BaseEntity<string>
    {
        public Country()
        {
            Towns = new List<Town>();
        }
        public virtual string Name { get; set; }
        public virtual IList<Town> Towns { get; set; }
    }

    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            HasMany(x => x.Towns).Inverse().Cascade.All();
            Table("Countries");
        }
    }
}