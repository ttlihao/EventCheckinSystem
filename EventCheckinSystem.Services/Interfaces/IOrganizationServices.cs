using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IOrganizationServices
    {
        Task<IEnumerable<OrganizationDTO>> GetAllOrganizationsAsync();
        Task<OrganizationDTO> GetOrganizationByIdAsync(int id);
        Task<OrganizationDTO> CreateOrganizationAsync(OrganizationDTO newOrganizationDto);
        Task<bool> UpdateOrganizationAsync(OrganizationDTO updatedOrganizationDto);
        Task<bool> DeleteOrganizationAsync(int id);
    }
}
