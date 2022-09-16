using APIEvent.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEvent.Filters
{
    public class AllowDeletEventActionFilter : ActionFilterAttribute
    {
        public readonly IEventsService _eventsService;
        public readonly IReservationsService _reservationsService;
        public AllowDeletEventActionFilter(IEventsService eventsService, IReservationsService reservationsService)
        {
            _eventsService = eventsService;
            _reservationsService = reservationsService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idEvent = (long)context.ActionArguments["idEvent"];

            var reservations = _reservationsService.GetReservationByIdEvent(idEvent);

            if (reservations.Count() > 0)
            {
                _eventsService.UpdateStatus(idEvent);
                var problem = new ProblemDetails
                {
                    Status = 400,
                    Title = "Bad Request",
                    Detail = "O evento não pode ser deletado, pois já existem reservas nele. Status foi inativado!",
                };
                context.Result = new ObjectResult(problem);
            }
        }
    }
}
