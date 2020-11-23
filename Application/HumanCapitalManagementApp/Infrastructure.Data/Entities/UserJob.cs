using FluentNHibernate.Mapping;
using System;

namespace Infrastructure.Data
{

    public class UserJob : BaseEntity<int>
    {
        public UserJob()
        {
            EndDate = null;
        }
        public virtual decimal MonthlySalary { get; set; }
        public virtual int WeekHours { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual User User { get; set; }
        public virtual User Manager { get; set; }
        public virtual Job Job { get; set; }
        public virtual CompanyAddress CompanyAddress { get; set; }
        public virtual Currency Currency { get; set; }
    }


    public class UserJobMap : ClassMap<UserJob>
    {
        public UserJobMap()
        {
            Id(x => x.Id)
                .GeneratedBy.TriggerIdentity()
                ;
            Map(x => x.MonthlySalary, "monthly_salary");
            Map(x => x.WeekHours, "week_hours");
            Map(x => x.StartDate, "start_date");
            Map(x => x.EndDate, "end_date").Nullable();
            References(x => x.User, "user_id").Cascade.None();
            References(x => x.Manager, "manager_id").Cascade.None();
            References(x => x.Job, "job_id").Cascade.None();
            References(x => x.CompanyAddress, "companyaddress_id").Cascade.None();
            References(x => x.Currency, "currency_id").Cascade.None();
            Table("UserJobs");
        }
    }
    //ПРАЖЯ VSICHKO DA E CascadeALLL!

}
