using Infrastructure.Data;
using System.Collections.Generic;

namespace Helpers
{
    public static class GlobalConstants
    {

        public static IDictionary<Gender, string> GenderNamePrefixes = new Dictionary<Gender, string>()
        {
            [Gender.Female] = "ms",
            [Gender.Male] = "mr"
        };

        public static string StatisticsCasheName = "statistics_home";
        public static string AdministratorRole = "Admin";
        
    }
}
