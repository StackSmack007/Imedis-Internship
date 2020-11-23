using DTO;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICountryService
    {
        Task<ICollection<CountryDTOout>> GetAllCountriesAsync();
        Task<Country> DeleteAsync(string countryId);
        Task CreateCountryAsync(CountryDTOin dto);
        Task<CountryEDITout> GetCountryForEditAsync(string id);
        Task<ICollection<CountryOptionDTOout>> GetAllCountryOptionsAsync();
        Task<Country> EditCountryAsync(CountryEDITout dto);
    }
}