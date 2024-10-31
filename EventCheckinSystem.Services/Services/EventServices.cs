using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;

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

        public async Task<IEnumerable<EventResponse>> GetAllEventsAsync()
        {
            var events = await _eventRepo.GetAllEventsAsync();
            return _mapper.Map<IEnumerable<EventResponse>>(events);
        }

        public async Task<EventResponse> GetEventByIdAsync(int id)
        {
            var eventEntity = await _eventRepo.GetEventByIdAsync(id);
            return _mapper.Map<EventResponse>(eventEntity);
        }

        public async Task<EventResponse> CreateEventAsync(CreateEventDTO newEventDto)
        {
            try
            {
                var newEvent = _mapper.Map<Event>(newEventDto);
                newEvent.CreatedBy = _userContextService.GetCurrentUserId();
                newEvent.LastUpdatedBy = newEvent.CreatedBy;
                newEvent.CreatedTime = _timeService.SystemTimeNow;
                newEvent.LastUpdatedTime = _timeService.SystemTimeNow;
                var createdEvent = await _eventRepo.CreateEventAsync(newEvent);
                return _mapper.Map<EventResponse>(createdEvent);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateEventAsync(EventDTO updatedEventDto)
        {
            try
            {
                var existingEvent = await _eventRepo.GetEventByIdAsync(updatedEventDto.EventID);
                if (existingEvent == null)
                {
                    throw new Exception("Event not found");
                }

                _mapper.Map(updatedEventDto, existingEvent); // Map updated fields to existing entity
                existingEvent.LastUpdatedBy = _userContextService.GetCurrentUserId();
                existingEvent.LastUpdatedTime = _timeService.SystemTimeNow;

                return await _eventRepo.UpdateEventAsync(existingEvent);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the event", ex);
            }
        }


        public async Task<bool> DeleteEventAsync(int id)
        {
            return await _eventRepo.DeleteEventAsync(id);
        }
    }

}
