using AutoMapper;
using DTO;
using Helpers.Interfaces;
using Infrastructure.Data;
using System;
using System.Linq;

namespace AutomapperCFG
{
    public class MapperConfigurator : Profile
    {
        public MapperConfigurator()
        {
            var allTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes());
            CreateMapToMappings(allTypes);
            CreateMapFromMappings(allTypes);

            CreateMap<User, UserDataDTOout>()
               .ForMember(d => d.UserRoles, opt => opt.MapFrom(s => s.Roles.Select(r => r.Name).ToArray()));

            CreateMap<User, UserProfileInfoDTOin>()
               .ForMember(d => d.PasswordHashed, opt => opt.MapFrom(s => s.Password))
               .ForMember(d => d.PasswordInputed, opt => opt.Ignore());


            CreateMap<Town, TownMoveDeleteDTOout>()
               .ForMember(d => d.IsDeletable, opt => opt.MapFrom(s => !s.Users.Any() && !s.CompanyAddresses.Any()))
               .ForMember(d => d.Deleted, opt => opt.Ignore());

            CreateMap<CompanyAddress, CompanyAddressDTOout>()
               .ForMember(d => d.WorkersCount, opt => opt.MapFrom(s => s.UserJobs.Count()));
        }

        private void CreateMapToMappings(System.Collections.Generic.IEnumerable<Type> allTypes)
        {
            Type[] sourseTypes = allTypes.Where(x => x.GetInterfaces()
                                         .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>)))
                                         .ToArray();
            foreach (Type sType in sourseTypes)
            {
                Type[] targetTypes = sType.GetInterfaces()
                                          .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>))
                                          .Select(x => x.GetGenericArguments().First())
                                          .ToArray();

                foreach (Type targetType in targetTypes)
                {
                    CreateMap(sType, targetType);
                }
            }
        }
        private void CreateMapFromMappings(System.Collections.Generic.IEnumerable<Type> allTypes)
        {
            Type[] destTypes = allTypes.Where(x => x.GetInterfaces()
                                       .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                                       .ToArray();

            foreach (Type dType in destTypes)
            {
                Type[] sourceTypes = dType.GetInterfaces()
                                          .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                                          .Select(x => x.GetGenericArguments().First())
                                          .ToArray();

                foreach (Type sType in sourceTypes)
                {
                    CreateMap(sType, dType);
                }
            }
        }
    }
}