using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IOrganizationServices
    {
        Task<IEnumerable<OrganizationDTO>> GetAllOrganizationsAsync();
        Task<OrganizationDTO> GetOrganizationByIdAsync(int id);
        Task<OrganizationDTO> CreateOrganizationAsync(CreateOrganizationDTO newOrganizationDto);
        Task<bool> UpdateOrganizationAsync(OrganizationDTO updatedOrganizationDto);
        Task<bool> DeleteOrganizationAsync(int id);
        Task<PagedResult<OrganizationDTO>> GetPagedOrganizationsAsync(PageRequest pageRequest);
    }
}
