using APIEvent.Core.Interfaces;
using APIEvent.Core.Models;
using APIEvent.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEvent.Filters
{
    public class AssureReservationNotExistActionFilter : ActionFilterAttribute
    {
        public readonly IReservationsService _reservationsService;
        public AssureReservationNotExistActionFilter(IReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            EventReservation eventReservation = (EventReservation)context.ActionArguments["eventReservation"];

            List<EventReservation> listReservation = _reservationsService.GetRervations();

            if(listReservation.Any(x => x.IdEvent == eventReservation.IdEvent && 
                                   x.PersonName == eventReservation.PersonName))
            {
                var problem = new ProblemDetails
                {
                    Status = 409,
                    Title = "Conflict",
                    Detail = "Esta reserva já existe no cadastro.",
                };
                context.Result = new ObjectResult(problem);
            }
        }
    }
}
