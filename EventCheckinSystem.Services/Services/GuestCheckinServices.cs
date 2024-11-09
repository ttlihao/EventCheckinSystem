using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Repo.Repositories.Implements;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class GuestCheckinServices : IGuestCheckinServices
    {
        private readonly IGuestCheckinRepo _checkinRepo;
        private readonly IGuestRepo _guestRepo;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;

        public GuestCheckinServices(IGuestCheckinRepo checkinRepo, IMapper mapper, IUserContextService userContextService, ITimeService timeService, IGuestRepo guestRepo)
        {
            _checkinRepo = checkinRepo;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
            _guestRepo = guestRepo;
        }

        public async Task<IEnumerable<GuestCheckinDTO>> GetAllCheckinsAsync()
        {
            var checkins = await _checkinRepo.GetAllCheckinsAsync();
            return _mapper.Map<IEnumerable<GuestCheckinDTO>>(checkins);
        }

        public async Task<GuestCheckinDTO> GetCheckinByIdAsync(int id)
        {
            var guestCheckin = await _checkinRepo.GetCheckinByIdAsync(id);
            return guestCheckin == null ? null : _mapper.Map<GuestCheckinDTO>(guestCheckin);
        }

        public async Task<GuestCheckinDTO> CreateCheckinAsync(int guestId)
        {
            try
            {
                var newCheckin = new GuestCheckin {
                    GuestID = guestId,
                    Notes = "",
                    Status = "Checkin Success",
                    Guest = await _guestRepo.GetGuestByIdAsync(guestId),
                    CreatedBy = _userContextService.GetCurrentUserId(),
                    LastUpdatedBy = _userContextService.GetCurrentUserId(),
                    CreatedTime = _timeService.SystemTimeNow,
                    LastUpdatedTime = _timeService.SystemTimeNow,
                    CheckinTime = _timeService.SystemTimeNow.DateTime,
                    IsActive = true,
                    IsDelete = false,
                };
                var createdCheckin = await _checkinRepo.CreateCheckinAsync(newCheckin);
                return _mapper.Map<GuestCheckinDTO>(createdCheckin);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateCheckinAsync(GuestCheckinDTO updatedCheckinDto)
        {
            try
            {
                var existingEvent = await _checkinRepo.GetCheckinByIdAsync(updatedCheckinDto.GuestCheckinID);
                if (existingEvent == null)
                {
                    throw new Exception("Checkin not found");
                }

                _mapper.Map(updatedCheckinDto, existingEvent); // Map updated fields to existing entity
                existingEvent.LastUpdatedBy = _userContextService.GetCurrentUserId();
                existingEvent.LastUpdatedTime = _timeService.SystemTimeNow;

                return await _checkinRepo.UpdateCheckinAsync(existingEvent);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the event", ex);
            }
        }

        public async Task<bool> DeleteCheckinAsync(int id)
        {
            if (await _checkinRepo.GetCheckinByIdAsync(id) == null)
            {
                throw new ArgumentException("Checkin Not Found!!");
            }
            return await _checkinRepo.DeleteCheckinAsync(id);
        }

        public async Task<GuestCheckinDTO> CheckinGuestByIdAsync(int guestId)
        {
            var checkin = await _checkinRepo.CheckinGuestByIdAsync(guestId, _userContextService.GetCurrentUserId());

            return _mapper.Map<GuestCheckinDTO>(checkin);
        }

        public async Task<PagedResult<GuestCheckinDTO>> GetPagedCheckinsAsync(PageRequest pageRequest)
        {
            var pagedCheckins = await _checkinRepo.GetPagedCheckinsAsync(pageRequest);

            return new PagedResult<GuestCheckinDTO>
            {
                Items = _mapper.Map<List<GuestCheckinDTO>>(pagedCheckins.Items),
                TotalCount = pagedCheckins.TotalCount,
                PageSize = pagedCheckins.PageSize,
                PageNumber = pagedCheckins.PageNumber
            };
        }

    }
}
