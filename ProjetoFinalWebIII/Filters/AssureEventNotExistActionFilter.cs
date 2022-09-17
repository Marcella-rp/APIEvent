using APIEvent.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using APIEvent.Core.Models;

namespace APIEvent.Filters
{
    public class AssureEventNotExistActionFilter : ActionFilterAttribute
    {
        public readonly IEventsService _eventsService;
        public AssureEventNotExistActionFilter(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            CityEvent cityEvent = (CityEvent)context.ActionArguments["cityEvent"];

            List<CityEvent> listEvent = _eventsService.GetEvents();

            if (listEvent.Any(x => (x.Title == cityEvent.Title &&
                                    x.DateHourEvent == cityEvent.DateHourEvent &&
                                    x.Local == cityEvent.Local)))
            {
                var problem = new ProblemDetails
                {
                    Status = 409,
                    Title = "Conflict",
                    Detail = "O evento já existe no cadastro"
                };
                context.Result = new ObjectResult(problem);
            }
        }
    }
}
