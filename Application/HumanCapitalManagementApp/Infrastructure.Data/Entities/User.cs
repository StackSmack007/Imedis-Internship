using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class User : BaseEntity<string>
    {
        public User()
        {
            IsDeleted = false;
            Roles = new List<Role>();
            UserJobs = new List<UserJob>();
            ManagedUsers = new List<UserJob>();
            Id = Guid.NewGuid().ToString();
        }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }
        public virtual int Gender { get; set; }
     //   public Gender GetGender => (Gender)Gender;
        public virtual string MailingAddress { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual Town ResidenceTown { get; set; }
        public virtual IList<Role> Roles { get; set; }
        public virtual IList<UserJob> UserJobs { get; set; }
        public virtual IList<UserJob> ManagedUsers { get; set; }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.Username,"username");
            Map(x => x.FirstName,"first_name");
            Map(x => x.MiddleName,"middle_name").Nullable();
            Map(x => x.LastName,"last_name");
            Map(x => x.Phone);
            Map(x => x.Password);          
            Map(x => x.Email);
            Map(x => x.Gender);
            Map(x => x.MailingAddress,"mailing_address");
            Map(x => x.DateOfBirth,"date_of_birth");
            Map(x => x.IsDeleted,"is_deleted");
            HasMany(x => x.UserJobs).Inverse().Cascade.All();
            HasMany(x => x.ManagedUsers).Inverse().Cascade.All();//kakvo stava tuka
            HasManyToMany(x => x.Roles).Cascade.All().Table("UserRoles");
            References(x => x.ResidenceTown,"residence_town_id").Cascade.None();
            Table("Users");
        }
    }
}