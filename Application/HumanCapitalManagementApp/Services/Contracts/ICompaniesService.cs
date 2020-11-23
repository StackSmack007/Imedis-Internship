using DTO;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICompaniesService
    {
        Task<ICollection<CompanyListItemDTOout>> GetAllCompaniesAsync();
        Task<Company> DeleteAsync(string id);
        Task<Company> CreateAsync(CompanyDTOin dto);
        Task<CompanyAddress> DeleteAddressAsync(int id);
        Task<ICollection<CompanyOfficeOptionDTOout>> GetAllCompanyOfficeOptionsAsync();
    }
}