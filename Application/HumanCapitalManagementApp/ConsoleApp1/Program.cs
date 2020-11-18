using Infrastructure.Data;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    //var country = session.Query<Country>().FirstOrDefault();

                    var country = new Country
                    {
                        Name = "Bulgaria",
                        Id = "BGN"
                    };

                    var town = new Town
                    {
                        Name = "Sofia",
                        PostalCode = "1000",
                        Country= country
                    };

                    session.Save(country);
                    session.Save(town);
                    transaction.Commit();
                }
            }
        }
    }
}
