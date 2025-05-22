using Business.Models;
using Persistence.Entities;
using Persistence.Repositories;

namespace Business.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;

    public async Task<EventResult> CreateEventAsync(CreateEventRequest request)
    {
        try
        {
            var eventEntity = new EventEntity
            {
                Image = request.Image,
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                EventDate = request.EventDate,
            };
            var result = await _eventRepository.AddAsync(eventEntity);
            return result.Success
                ? new EventResult { Success = true }
                : new EventResult { Success = false, Error = result.Error };
        }
        catch (Exception ex)
        {
            return new EventResult()
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

    public async Task<EventResult<IEnumerable<Event>>> GetEventsAsync()
    {
        var result = await _eventRepository.GetAllAsync();
        var events = result.Result?.Select(x => new Event
        {
            Image = x.Image,
            Title = x.Title,
            Description = x.Description,
            Location = x.Location,
            EventDate = x.EventDate,
        });
        return new EventResult<IEnumerable<Event>> { Success = true, Result = events };
    }
    
    public async Task<EventResult<Event>?> GetEventsAsync(string eventId)
    {
        var result = await _eventRepository.GetAsync(x => x.Id == eventId);

        if (!result.Success || result.Result == null)
            return new EventResult<Event?> { Success = false, Error = "Event Not found" };
        var currentEvent = new Event
        {
            Image = result.Result.Image,
            Title = result.Result.Title,
            Description = result.Result.Description,
            Location = result.Result.Location,
            EventDate = result.Result.EventDate
        };

        return new EventResult<Event?> { Success = true, Result = currentEvent };

    }
}

