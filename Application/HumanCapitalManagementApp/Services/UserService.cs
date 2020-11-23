using DTO;
using System;
using AutoMapper;
using Helpers.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Services.Contracts;
using Services.Extensions;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using NHibernate.Linq;
using System.Collections.Generic;

namespace Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IEncrypter encrypter;
        private const string AUTH_COOKIE_NAME = "UserAuth";
        public UserService(IHttpContextAccessor httpContextAccessor, IMapper mapper, IEncrypter encrypter) : base(mapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.encrypter = encrypter;
        }
        public bool IsLoggedIn =>
            this.httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(AUTH_COOKIE_NAME);
        public UserDataDTOout User
        {
            get
            {
                string userInfoEncrypted = string.Empty;
                if (!this.httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(AUTH_COOKIE_NAME, out userInfoEncrypted))
                {
                    return null;
                }
                return JsonConvert.DeserializeObject<UserDataDTOout>(encrypter.Decrypt(userInfoEncrypted));
            }
        }

        public async Task<UserDataDTOout> LoginAsync(UserLoginDTOin data)
        {
            this.Logout();
            ValidatePassword(data.Password);
            User user = null;
            using (var session = NhibernateHelper.OpenSession())
            {
                user = await session.Query<User>().FirstOrDefaultAsync(x => (data.UsernameOrEmail == x.Username || data.UsernameOrEmail == x.Email) && data.Password.ToHashedString() == x.Password);
                if (user is null)
                {
                    return null;
                }
                var userData = mapper.Map<UserDataDTOout>(user);
                this.httpContextAccessor
                    .HttpContext
                    .Response
                    .Cookies
                    .Append(AUTH_COOKIE_NAME, encrypter.Encrypt(JsonConvert.SerializeObject(userData)));
                return userData;
            }
        }

        public void Logout()
        {
            if (httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(AUTH_COOKIE_NAME))
            {
                this.httpContextAccessor.HttpContext.Response.Cookies.Delete(AUTH_COOKIE_NAME);
            }
        }

        public async Task<UserDataDTOout> RegisterAsync(UserRegisterDTOin data)
        {
            ValidatePassword(data.Password);
            User user = mapper.Map<User>(data);
            user.Password = user.Password.ToHashedString();
            using (var session = NhibernateHelper.OpenSession())
            {
                bool usernameTaken = await session
                    .Query<User>()
                    .AnyAsync(x => x.Username == data.Username);

                if (usernameTaken)
                    throw new ArgumentException("Username is taken!");

                bool emailTaken = await session
                                        .Query<User>()
                                        .AnyAsync(x => x.Email == data.Email);
                if (emailTaken)
                    throw new ArgumentException("Email is taken!");

                user.ResidenceTown = await session.Query<Town>().FirstOrDefaultAsync(x => x.Id == data.ResidenceTownId);
                user.Roles.Add(await session.Query<Role>().FirstOrDefaultAsync(x => x.Name == "User"));
                using (var transaction = session.BeginTransaction())
                {
                    await session.SaveAsync(user);
                    await transaction.CommitAsync();
                }
            }

            return await LoginAsync(new UserLoginDTOin(data.Username, data.Password));
        }

        private void ValidatePassword(string pwd)
        {
            var paswordIsValid = pwd.Length >= 4 && pwd.Any(x => Char.IsDigit(x));
            if (!paswordIsValid)
                throw new InvalidOperationException("Password was invalid");
        }

        public async Task<UserProfileInfoDTOout> GetProfileDataAsync(string username)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                return await session
                                .Query<User>()
                                .Where(x => (x.Username == username))
                                .ProjectTo<UserProfileInfoDTOout>(mapper.ConfigurationProvider)
                                .FirstOrDefaultAsync();
            }
        }

        public async Task<UserProfileInfoDTOin> GetMyProfileInfoForEditAsync()
        {
            if (!this.IsLoggedIn)
            {
                return null;
            }

            using (var session = NhibernateHelper.OpenSession())
            {
                return await session
                                .Query<User>()
                                .Where(x => (x.Username == this.User.UserName))
                                .ProjectTo<UserProfileInfoDTOin>(mapper.ConfigurationProvider)
                                .FirstOrDefaultAsync();
            }
        }

        public async Task<bool> UpdateUserDataAsync(UserProfileInfoDTOin dto)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                User userFd = await session.Query<User>().FirstOrDefaultAsync(x => x.Username == dto.Username);
                Town townFd = await session.Query<Town>().FirstOrDefaultAsync(x => x.Id == dto.ResidenceTownId);
                if (userFd is null || townFd is null)
                {
                    return false;
                }
                userFd.FirstName = dto.FirstName;
                userFd.LastName = dto.LastName;
                userFd.MiddleName = dto.MiddleName;
                userFd.Email = dto.Email;
                userFd.Phone = dto.Phone;
                userFd.MailingAddress = dto.MailingAddress;
                userFd.Gender = (int)dto.Gender;
                userFd.DateOfBirth = dto.DateOfBirth;
                userFd.ResidenceTown = townFd;
                using (var transaction = session.BeginTransaction())
                {
                    await session.UpdateAsync(userFd);
                    await transaction.CommitAsync();
                }
                return true;
            }
        }

        public async Task<ICollection<UserOptionDTOout>> GetAllUsersOptionsAsync()
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                return await session.Query<User>().ProjectTo<UserOptionDTOout>(mapper.ConfigurationProvider).ToListAsync();
            }
        }
    }
}