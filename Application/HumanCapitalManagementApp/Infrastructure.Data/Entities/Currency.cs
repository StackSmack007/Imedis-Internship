using FluentNHibernate.Mapping;

namespace Infrastructure.Data
{
    public class Currency : BaseEntity<int>
    {
        public virtual string Name { get; set; }
        public virtual decimal Rate { get; set; }
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
            Table("Currencies");
        }
    }

}
