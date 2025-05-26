using Business.Models;

namespace Business.Services;

public interface IEventService
{
    Task<EventResult> CreateEventAsync(CreateEventRequest request);
    Task<EventResult<IEnumerable<Event>>> GetEventsAsync();  
    Task<EventResult<Event?>> GetEventAsync(string eventId);
}