using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class EventsController(IEventService eventService) : ControllerBase
{
    private readonly IEventService _eventService = eventService;
}