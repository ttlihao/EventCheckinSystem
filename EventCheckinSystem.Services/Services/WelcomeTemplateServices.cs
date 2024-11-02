using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class WelcomeTemplateServices : IWelcomeTemplateServices
    {
        private readonly IWelcomeTemplateRepo _welcomeTemplateRepo;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;

        public WelcomeTemplateServices(IWelcomeTemplateRepo welcomeTemplateRepo, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _welcomeTemplateRepo = welcomeTemplateRepo;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<IEnumerable<WelcomeTemplateDTO>> GetAllWelcomeTemplatesAsync()
        {
            var templates = await _welcomeTemplateRepo.GetAllWelcomeTemplatesAsync();
            return _mapper.Map<IEnumerable<WelcomeTemplateDTO>>(templates);
        }

        public async Task<WelcomeTemplateDTO> GetWelcomeTemplateByIdAsync(int id)
        {
            var template = await _welcomeTemplateRepo.GetWelcomeTemplateByIdAsync(id);
            return template != null ? _mapper.Map<WelcomeTemplateDTO>(template) : null;
        }

        public async Task<WelcomeTemplateDTO> CreateWelcomeTemplateAsync(CreateWelcomeTemplateDTO welcomeTemplateDto)
        {
            try
            {
                var newTemplate = _mapper.Map<WelcomeTemplate>(welcomeTemplateDto);
                newTemplate.CreatedBy = _userContextService.GetCurrentUserId();
                newTemplate.LastUpdatedBy = newTemplate.CreatedBy;
                newTemplate.CreatedTime = _timeService.SystemTimeNow;
                newTemplate.LastUpdatedTime = _timeService.SystemTimeNow;
                var createdTemplate = await _welcomeTemplateRepo.CreateWelcomeTemplateAsync(newTemplate);
                return _mapper.Map<WelcomeTemplateDTO>(createdTemplate);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the welcome template", ex);
            }
        }

        public async Task<bool> UpdateWelcomeTemplateAsync(WelcomeTemplateDTO welcomeTemplateDto)
        {
            try
            {
                var existingTemplate = await _welcomeTemplateRepo.GetWelcomeTemplateByIdAsync(welcomeTemplateDto.WelcomeTemplateID);
                if (existingTemplate == null)
                {
                    throw new Exception("WelcomeTemplate not found");
                }
                _mapper.Map(welcomeTemplateDto, existingTemplate); // Map updated fields to existing entity
                existingTemplate.LastUpdatedBy = _userContextService.GetCurrentUserId();
                existingTemplate.LastUpdatedTime = _timeService.SystemTimeNow;
                return await _welcomeTemplateRepo.UpdateWelcomeTemplateAsync(existingTemplate);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the welcome template", ex);
            }
        }

        public async Task<bool> DeleteWelcomeTemplateAsync(int id)
        {
            try
            {
                return await _welcomeTemplateRepo.DeleteWelcomeTemplateAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the welcome template", ex);
            }
        }

        public async Task<IEnumerable<WelcomeTemplateDTO>> GetWelcomeTemplatesByGuestGroupAsync(int guestGroupId)
        {
            var templates = await _welcomeTemplateRepo.GetWelcomeTemplatesByGuestGroupAsync(guestGroupId);
            return _mapper.Map<IEnumerable<WelcomeTemplateDTO>>(templates);
        }
    }
}
