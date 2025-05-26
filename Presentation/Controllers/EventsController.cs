using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class EventsController(IEventService eventService, ILogger<EventsController> logger) : ControllerBase
{
    private readonly IEventService _eventService = eventService;
    
    private readonly ILogger<EventsController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("This is an info message");
        var events = await _eventService.GetEventsAsync();
        return Ok(events);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var currentEvent = await _eventService.GetEventAsync(id);
        return Ok(currentEvent);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateEventRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await _eventService.CreateEventAsync(request);
        return result.Success ? Ok() : StatusCode(500, result.Error);
    }
}