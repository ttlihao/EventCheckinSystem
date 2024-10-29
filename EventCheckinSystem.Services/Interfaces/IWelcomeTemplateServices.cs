﻿using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IWelcomeTemplateServices
    {
        Task<IEnumerable<WelcomeTemplateDTO>> GetAllWelcomeTemplatesAsync();
        Task<WelcomeTemplateDTO> GetWelcomeTemplateByIdAsync(int id);
        Task<WelcomeTemplateDTO> CreateWelcomeTemplateAsync(WelcomeTemplateDTO welcomeTemplateDto);
        Task<bool> UpdateWelcomeTemplateAsync(WelcomeTemplateDTO welcomeTemplateDto);
        Task<bool> DeleteWelcomeTemplateAsync(int id);
        Task<IEnumerable<WelcomeTemplateDTO>> GetWelcomeTemplatesByGuestGroupAsync(int guestGroupId);
    }
}
