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
    public class GuestServices : IGuestServices
    {
        private readonly IGuestRepo _guestRepo;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;

        public GuestServices(IGuestRepo guestRepo, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _guestRepo = guestRepo;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<List<GuestDTO>> GetAllGuestsAsync()
        {
            var guests = await _guestRepo.GetAllGuestsAsync();
            return _mapper.Map<List<GuestDTO>>(guests);
        }

        public async Task<GuestDTO> GetGuestByIdAsync(int guestId)
        {
            var guest = await _guestRepo.GetGuestByIdAsync(guestId);
            return guest != null ? _mapper.Map<GuestDTO>(guest) : null;
        }

        public async Task<GuestDTO> AddGuestAsync(GuestDTO guestDto)
        {
            try
            {
                var newGuest = _mapper.Map<Guest>(guestDto);
                newGuest.CreatedBy = _userContextService.GetCurrentUserId();
                newGuest.LastUpdatedBy = newGuest.CreatedBy;
                newGuest.CreatedTime = _timeService.SystemTimeNow;
                newGuest.LastUpdatedTime = _timeService.SystemTimeNow;
                var createdGuest = await _guestRepo.AddGuestAsync(newGuest);
                return _mapper.Map<GuestDTO>(createdGuest);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the guest", ex);
            }
        }

        public async Task<bool> UpdateGuestAsync(GuestDTO guestDto)
        {
            try
            {
                var existingGuest = await _guestRepo.GetGuestByIdAsync(guestDto.GuestID);
                if (existingGuest == null)
                {
                    throw new Exception("Guest not found");
                }
                _mapper.Map(guestDto, existingGuest); // Map updated fields to existing entity
                existingGuest.LastUpdatedBy = _userContextService.GetCurrentUserId();
                existingGuest.LastUpdatedTime = _timeService.SystemTimeNow;
                return await _guestRepo.UpdateGuestAsync(existingGuest);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the guest", ex);
            }
        }

        public async Task<bool> DeleteGuestAsync(int guestId)
        {
            try
            {
                return await _guestRepo.DeleteGuestAsync(guestId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the guest", ex);
            }
        }

        public async Task<List<GuestDTO>> GetGuestsByGroupIdAsync(int guestGroupId)
        {
            var guests = await _guestRepo.GetGuestsByGroupIdAsync(guestGroupId);
            return _mapper.Map<List<GuestDTO>>(guests);
        }
    }
}
