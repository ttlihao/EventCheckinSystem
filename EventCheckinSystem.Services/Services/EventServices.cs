﻿using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class EventServices : IEventServices
    {
        private readonly IEventRepo _eventRepo;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;

        public EventServices(IEventRepo eventRepo, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _eventRepo = eventRepo;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService; 
        }

        public async Task<IEnumerable<EventDTO>> GetAllEventsAsync()
        {
            var events = await _eventRepo.GetAllEventsAsync();
            return _mapper.Map<IEnumerable<EventDTO>>(events);
        }

        public async Task<EventDTO> GetEventByIdAsync(int id)
        {
            var eventEntity = await _eventRepo.GetEventByIdAsync(id);
            return _mapper.Map<EventDTO>(eventEntity);
        }

        public async Task<EventDTO> CreateEventAsync(CreateEventDTO newEventDto)
        {
            try
            {
                var newEvent = _mapper.Map<Event>(newEventDto);
                newEvent.CreatedBy = _userContextService.GetCurrentUserId();
                newEvent.LastUpdatedBy = newEvent.CreatedBy;
                newEvent.CreatedTime = _timeService.SystemTimeNow;
                newEvent.LastUpdatedTime = _timeService.SystemTimeNow;
                var createdEvent = await _eventRepo.CreateEventAsync(newEvent);
                return _mapper.Map<EventDTO>(createdEvent);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateEventAsync(EventDTO updatedEventDto)
        {
            try
            {
                var existingEvent = _mapper.Map<Event>(updatedEventDto);
                existingEvent.LastUpdatedBy = _userContextService.GetCurrentUserId();
                existingEvent.LastUpdatedTime = _timeService.SystemTimeNow;
                await _eventRepo.UpdateEventAsync(existingEvent);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteEventAsync(int id)
        {
            await _eventRepo.DeleteEventAsync(id);
        }
    }

}
