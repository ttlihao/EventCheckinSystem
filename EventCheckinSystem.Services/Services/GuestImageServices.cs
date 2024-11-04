using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class GuestImageServices : IGuestImageServices
    {
        private readonly IGuestImageRepo _guestImageRepo;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;

        public GuestImageServices(IGuestImageRepo guestImageRepo, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _guestImageRepo = guestImageRepo;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<IEnumerable<GuestImageDTO>> GetAllGuestImagesAsync()
        {
            var guestImages = await _guestImageRepo.GetAllGuestImagesAsync();
            return _mapper.Map<IEnumerable<GuestImageDTO>>(guestImages);
        }

        public async Task<GuestImageDTO> GetGuestImageByIdAsync(int id)
        {
            var guestImage = await _guestImageRepo.GetGuestImageByIdAsync(id);
            return _mapper.Map<GuestImageDTO>(guestImage);
        }

        public async Task<GuestImageDTO> CreateGuestImageAsync(CreateGuestImageDTO guestImageDto)
        {
            try
            {
                var newGuestImage = _mapper.Map<GuestImage>(guestImageDto);
                newGuestImage.CreatedBy = _userContextService.GetCurrentUserId();
                newGuestImage.LastUpdatedBy = newGuestImage.CreatedBy;
                newGuestImage.CreatedTime = _timeService.SystemTimeNow;
                newGuestImage.LastUpdatedTime = _timeService.SystemTimeNow;
                var createdGuestImage = await _guestImageRepo.CreateGuestImageAsync(newGuestImage);
                return _mapper.Map<GuestImageDTO>(createdGuestImage);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the guest image", ex);
            }
        }

        public async Task<bool> UpdateGuestImageAsync(GuestImageDTO guestImageDto)
        {
            try
            {
                var existingGuestImage = await _guestImageRepo.GetGuestImageByIdAsync(guestImageDto.GuestImageID);
                if (existingGuestImage == null)
                {
                    throw new Exception("GuestImage not found");
                }
                _mapper.Map(guestImageDto, existingGuestImage); // Map updated fields to existing entity
                existingGuestImage.LastUpdatedBy = _userContextService.GetCurrentUserId();
                existingGuestImage.LastUpdatedTime = _timeService.SystemTimeNow;
                return await _guestImageRepo.UpdateGuestImageAsync(existingGuestImage);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the guest image", ex);
            }
        }

        public async Task<bool> DeleteGuestImageAsync(int id)
        {
            try
            {
                return await _guestImageRepo.DeleteGuestImageAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the guest image", ex);
            }
        }

        public async Task<PagedResult<GuestImageDTO>> GetPagedGuestImagesAsync(PageRequest pageRequest)
        {
            var pagedGuestImages = await _guestImageRepo.GetPagedGuestImagesAsync(pageRequest);
            return new PagedResult<GuestImageDTO>
            {
                Items = _mapper.Map<List<GuestImageDTO>>(pagedGuestImages.Items),
                TotalCount = pagedGuestImages.TotalCount,
                PageSize = pagedGuestImages.PageSize,
                PageNumber = pagedGuestImages.PageNumber
            };
        }
    }
}
