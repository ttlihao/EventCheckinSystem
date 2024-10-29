using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Implements;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class GuestGroupServices : IGuestGroupServices
    {
        private readonly IGuestGroupRepo _guestGroupRepo;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;

        public GuestGroupServices(IGuestGroupRepo guestGroupRepo, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _guestGroupRepo = guestGroupRepo;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<IEnumerable<GuestGroupDTO>> GetAllGuestGroupsAsync()
        {
            var guestGroups = await _guestGroupRepo.GetAllGuestGroupsAsync();

            return _mapper.Map<IEnumerable<GuestGroupDTO>>(guestGroups);
        }

        public async Task<GuestGroupDTO> GetGuestGroupByIdAsync(int id)
        {
            var guestGroup = await _guestGroupRepo.GetGuestGroupByIdAsync(id);

            return guestGroup == null ? null : _mapper.Map<GuestGroupDTO>(guestGroup);
        }

        public async Task<GuestGroupDTO> CreateGuestGroupAsync(GuestGroupDTO guestGroupDto)
        {
            try
            {
                var newGuestGroup = _mapper.Map<GuestGroup>(guestGroupDto);
                newGuestGroup.CreatedBy = _userContextService.GetCurrentUserId();
                newGuestGroup.LastUpdatedBy = newGuestGroup.CreatedBy;
                newGuestGroup.CreatedTime = _timeService.SystemTimeNow;
                newGuestGroup.LastUpdatedTime = _timeService.SystemTimeNow;
                var createdGuestGroup = await _guestGroupRepo.CreateGuestGroupAsync(newGuestGroup);
                return _mapper.Map<GuestGroupDTO>(createdGuestGroup);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateGuestGroupAsync(GuestGroupDTO guestGroupDto)
        {
            try
            {
                var existingGuestGroup = await _guestGroupRepo.GetGuestGroupByIdAsync(guestGroupDto.EventID);
                if (existingGuestGroup == null)
                {
                    throw new Exception("Guest Group not found");
                }

                _mapper.Map(guestGroupDto, existingGuestGroup); // Map updated fields to existing entity
                existingGuestGroup.LastUpdatedBy = _userContextService.GetCurrentUserId();
                existingGuestGroup.LastUpdatedTime = _timeService.SystemTimeNow;

                return await _guestGroupRepo.UpdateGuestGroupAsync(existingGuestGroup);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the guest group", ex);
            }
        }

        public async Task<bool> DeleteGuestGroupAsync(int id)
        {
            return await _guestGroupRepo.DeleteGuestGroupAsync(id);
        }

        public async Task<GuestGroupDTO> GetGuestGroupByGuestIdAsync(int guestId)
        {
            var guestGroup = await _guestGroupRepo.GetGuestGroupByGuestIdAsync(guestId);

            return guestGroup == null ? null : _mapper.Map<GuestGroupDTO>(guestGroup);
        }
    }
}
