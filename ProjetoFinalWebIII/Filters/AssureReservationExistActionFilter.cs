using APIEvent.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEvent.Filters
{
    public class AssureReservationExistActionFilter : ActionFilterAttribute
    {
        public readonly IReservationsService _reservationsService;
        public AssureReservationExistActionFilter(IReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idReservation = (long)context.ActionArguments["idReservation"];

            if (_reservationsService.GetRervationById(idReservation) == null)
            {
                var problem = new ProblemDetails
                {
                    Status = 404,
                    Title = "Not Found",
                    Detail = "Não foi possível encontrar a reserva",
                };
                context.Result = new ObjectResult(problem);
            }
        }
    }
}
