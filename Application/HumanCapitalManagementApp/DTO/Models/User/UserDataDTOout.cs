using System;
using System.Linq;

namespace DTO
{
    public class UserDataDTOout
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string[] UserRoles { get; set; }

        public bool IsInRole(string roleName, bool caseInsensitive = true)
        {
            if (caseInsensitive)
            {
                return UserRoles.Any(x => x.ToLower() == roleName.ToLower());
            }

            return UserRoles.Contains(roleName);
        }
    }
}