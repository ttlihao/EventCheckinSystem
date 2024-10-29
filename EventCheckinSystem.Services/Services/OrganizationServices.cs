using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class OrganizationServices : IOrganizationServices
    {
        private readonly IOrganizationRepo _organizationRepo;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;

        public OrganizationServices(IOrganizationRepo organizationRepo, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _organizationRepo = organizationRepo;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<IEnumerable<OrganizationDTO>> GetAllOrganizationsAsync()
        {
            var organizations = await _organizationRepo.GetAllOrganizationsAsync();
            return _mapper.Map<IEnumerable<OrganizationDTO>>(organizations);
        }

        public async Task<OrganizationDTO> GetOrganizationByIdAsync(int id)
        {
            var organizationEntity = await _organizationRepo.GetOrganizationByIdAsync(id);
            return _mapper.Map<OrganizationDTO>(organizationEntity);
        }

        public async Task<CreateOrganizationDTO> CreateOrganizationAsync(CreateOrganizationDTO newOrganizationDto)
        {
            try
            {
                var newOrganization = _mapper.Map<Organization>(newOrganizationDto);
                newOrganization.CreatedBy = _userContextService.GetCurrentUserId();
                newOrganization.LastUpdatedBy = newOrganization.CreatedBy;
                newOrganization.CreatedTime = _timeService.SystemTimeNow;
                newOrganization.LastUpdatedTime = _timeService.SystemTimeNow;
                var createdOrganization = await _organizationRepo.CreateOrganizationAsync(newOrganization);
                return _mapper.Map<CreateOrganizationDTO>(createdOrganization);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the organization", ex);
            }
        }

        public async Task<bool> UpdateOrganizationAsync(OrganizationDTO updatedOrganizationDto)
        {
            try
            {
                var existingOrganization = await _organizationRepo.GetOrganizationByIdAsync(updatedOrganizationDto.OrganizationID);
                if (existingOrganization == null)
                {
                    throw new Exception("Organization not found");
                }
                _mapper.Map(updatedOrganizationDto, existingOrganization); // Map updated fields to existing entity
                existingOrganization.LastUpdatedBy = _userContextService.GetCurrentUserId();
                existingOrganization.LastUpdatedTime = _timeService.SystemTimeNow;
                return await _organizationRepo.UpdateOrganizationAsync(existingOrganization);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the organization", ex);
            }
        }

        public async Task<bool> DeleteOrganizationAsync(int id)
        {
            try
            {
                return await _organizationRepo.DeleteOrganizationAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the organization", ex);
            }
        }
    }
}
