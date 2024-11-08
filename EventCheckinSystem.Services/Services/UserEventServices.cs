using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
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
        private readonly IAuthenticateRepo _authenticateRepo;
        private readonly IEventRepo _eventRepo;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;

        public UserEventServices(IUserEventRepo userEventRepo, IAuthenticateRepo authenticateRepo, IEventRepo eventRepo, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _userEventRepo = userEventRepo;
            _authenticateRepo = authenticateRepo;
            _eventRepo = eventRepo; 
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<List<UserEventResponse>> GetAllUserEventsAsync()
        {
            var userEvents = await _userEventRepo.GetAllUserEventsAsync();
            return _mapper.Map<List<UserEventResponse>>(userEvents);
        }

        public async Task<UserEventResponse> GetUserEventByIdAsync(int eventId)
        {
            var userEvent = await _userEventRepo.GetUserEventByIdAsync(eventId);
            return userEvent != null ? _mapper.Map<UserEventResponse>(userEvent) : null;
        }

        public async Task<UserEventResponse> AddUserEventAsync(UserEventDTO userEventDto)
        {
            try
            {
                var newUserEvent = _mapper.Map<UserEvent>(userEventDto);
                newUserEvent.User = await _authenticateRepo.GetUsesByIdAsync(newUserEvent.UserID);
                newUserEvent.Event = await _eventRepo.GetEventByIdAsync(newUserEvent.EventID);
                var createdUserEvent = await _userEventRepo.AddUserEventAsync(newUserEvent);
                return _mapper.Map<UserEventResponse>(createdUserEvent);
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

        public async Task<PagedResult<UserEventResponse>> GetPagedUserEventsAsync(PageRequest pageRequest)
        {
            var pagedUserEvents = await _userEventRepo.GetPagedUserEventsAsync(pageRequest);
            return new PagedResult<UserEventResponse>
            {
                Items = _mapper.Map<List<UserEventResponse>>(pagedUserEvents.Items),
                TotalCount = pagedUserEvents.TotalCount,
                PageSize = pagedUserEvents.PageSize,
                PageNumber = pagedUserEvents.PageNumber
            };
        }

        public async Task<List<EventResponse>> GetEventsByUserIdAsync(string userId)
        {
            var eventIds = await _userEventRepo.GetEventIdsByUserIdAsync(userId);
            var events = await _eventRepo.GetEventsByEventIdsAsync(eventIds);       
            return _mapper.Map<List<EventResponse>>(events);
        }

    }
}
