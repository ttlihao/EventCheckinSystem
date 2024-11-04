using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IOrganizationRepo
    {
        Task<IEnumerable<Organization>> GetAllOrganizationsAsync();
        Task<Organization> GetOrganizationByIdAsync(int id);
        Task<Organization> CreateOrganizationAsync(Organization newOrganizationDto);
        Task<bool> UpdateOrganizationAsync(Organization updatedOrganizationDto);
        Task<bool> DeleteOrganizationAsync(int id);
        Task<PagedResult<Organization>> GetPagedOrganizationsAsync(PageRequest pageRequest);
    }
}
