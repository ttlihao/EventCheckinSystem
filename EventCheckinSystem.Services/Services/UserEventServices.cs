﻿using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class UserEventServices : IUserEventServices
    {
        private readonly IUserEventRepo _userEventRepo;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;

        public UserEventServices(IUserEventRepo userEventRepo, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _userEventRepo = userEventRepo;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<List<UserEventDTO>> GetAllUserEventsAsync()
        {
            var userEvents = await _userEventRepo.GetAllUserEventsAsync();
            return _mapper.Map<List<UserEventDTO>>(userEvents);
        }

        public async Task<UserEventDTO> GetUserEventByIdAsync(int eventId)
        {
            var userEvent = await _userEventRepo.GetUserEventByIdAsync(eventId);
            return userEvent != null ? _mapper.Map<UserEventDTO>(userEvent) : null;
        }

        public async Task<UserEventDTO> AddUserEventAsync(UserEventDTO userEventDto)
        {
            try
            {
                var newUserEvent = _mapper.Map<UserEvent>(userEventDto);
                var createdUserEvent = await _userEventRepo.AddUserEventAsync(newUserEvent);
                return _mapper.Map<UserEventDTO>(createdUserEvent);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the user event", ex);
            }
        }

        public async Task<bool> UpdateUserEventAsync(UserEventDTO userEventDto)
        {
            try
            {
                var existingUserEvent = await _userEventRepo.GetUserEventByIdAsync(userEventDto.EventID);
                if (existingUserEvent == null)
                {
                    throw new Exception("UserEvent not found");
                }
                _mapper.Map(userEventDto, existingUserEvent); // Map updated fields to existing entity
                return await _userEventRepo.UpdateUserEventAsync(existingUserEvent);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the user event", ex);
            }
        }

        public async Task<bool> DeleteUserEventAsync(int eventId)
        {
            try
            {
                return await _userEventRepo.DeleteUserEventAsync(eventId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the user event", ex);
            }
        }
    }
}
