using Helpers.Interfaces;
using Infrastructure.Data;

namespace DTO
{
    public class UserOptionDTOout : IMapFrom<User>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
    }
}
