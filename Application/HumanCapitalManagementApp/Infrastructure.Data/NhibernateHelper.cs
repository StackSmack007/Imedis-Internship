using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Infrastructure.Data
{
 public class NhibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory is null)
                    InitializeSessionFactory();
                return _sessionFactory;
            }
        }
        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently
                .Configure()
                .Database(PostgreSQLConfiguration.Standard
                    .ConnectionString(c => c
                 .Host("localhost")
                 .Port(5433)
                 .Database("test")
                 .Username("postgres")
                 .Password("1234A43B#")
                ).ShowSql())
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Country>())
            .ExposeConfiguration(
               cfg =>new SchemaUpdate(cfg)                
            //   cfg=>new SchemaExport(cfg).Create(true,true)                
                )
            .BuildSessionFactory();
        }
      
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

    }
}
