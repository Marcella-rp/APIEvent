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
    public class ReservationController : ControllerBase
    {
        public IReservationsService _reservationsService;
        public ReservationController(IReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }

        [HttpGet("/Reservations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<List<EventReservation>> GetEvents()
        {
            return Ok(_reservationsService.GetRervations());
        }

        [HttpGet("/Reservations/{idReservation}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<EventReservation> GetReservationById(long idReservation)
        {
            var reservations = _reservationsService.GetRervationById(idReservation);
            if (reservations == null)
            {
                return NotFound();
            }
            return Ok(reservations);
        }

        [HttpGet("/Reservations/personNameAndTitle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]

        public ActionResult<EventReservation> GetEventByPersonName(string personName, string title)
        {
            var reservations = _reservationsService.GetRervationByPersonNameTitle(personName,title);
            if (!reservations.Any())
            {
                return NotFound();
            }
            return Ok(reservations);
        }

        [HttpPost("/Reservations")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(AssureReservationNotExistActionFilter))]
        [Authorize]
        public ActionResult<EventReservation> PostReservations(EventReservation eventReservation)
        {
            if (!_reservationsService.InsertReservation(eventReservation))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(PostReservations), eventReservation);
        }

        [HttpPut("/Reservations")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(AssureReservationExistActionFilter))]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateReservation(long idReservation, EventReservation eventReservation)
        {
            if (!_reservationsService.UpdateReservation(idReservation, eventReservation))
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPut("/Reservations / quantity")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(AssureReservationExistActionFilter))]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateReservationQuantity(long idReservation, long quantity)
        {
            if (!_reservationsService.UpdateReservationQuantity(idReservation, quantity))
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("/Reservations")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteReservation(long idReservation)
        {
            if (!_reservationsService.DeleteReservation(idReservation))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}