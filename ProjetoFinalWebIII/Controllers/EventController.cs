using APIEvent.Core.Interfaces;
using APIEvent.Core.Models;
using APIEvent.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace APIEvent.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    public class EventController : ControllerBase
    {
        public IEventsService _eventsService;
        public EventController (IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpGet("/Events")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetEvents()
        {
            return Ok(_eventsService.GetEvents());
        }

        [HttpGet("/Events/id/{idEvent}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult <CityEvent> GetEventById(long idEvent)
        {
            var events = _eventsService.GetEventById(idEvent);
            if(events == null)
            {
                return NotFound();
            }
            return Ok(events);
        }

        [HttpGet("/Events/title/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult <CityEvent> GetEventByTitle(string title)
        {
            var events = _eventsService.GetEventByTitle(title);
            if (!events.Any())
            {
                return NotFound();
            }
            return Ok(events);
        }

        [HttpGet("/Events/localAndDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<CityEvent> GetEventByLocalDate(string local, DateTime date)
        {
            var events = _eventsService.GetEventByLocalDate(local,date);
            if (!events.Any())
            {
                return NotFound();
            }
            return Ok(events);
        }

       [HttpGet("/Events/priceRangeAndDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<CityEvent> GetEventByRangePriceDate(decimal inicialPrice, decimal finalPrice, DateTime date)
        {
            var events = _eventsService.GetEventByRangePriceDate(inicialPrice,finalPrice,date);
            if (!events.Any())
            {
                return NotFound();
            }
            return Ok(events);
        }

        [HttpPost ("/Events")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(AssureEventNotExistActionFilter))]
        [Authorize(Roles = "admin")]
        public ActionResult<CityEvent> PostEvents(CityEvent cityEvent)
        {
            if (!_eventsService.InsertEvent(cityEvent))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(PostEvents), cityEvent);
        }

        [HttpPut ("/Events")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(AssureEventExistActionFilter))]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateEvent (long IdEvent, CityEvent cityEvent)
        {
            if (!_eventsService.UpdateEvent(IdEvent,cityEvent))
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("/Events")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(AllowDeletEventActionFilter))]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteEvent(long idEvent)
        {
            if (!_eventsService.DeleteEvent(idEvent))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}