using APIEvent.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace APIEvent.Filters
{
    public class AssureEventExistActionFilter : ActionFilterAttribute
    {
        public readonly IEventsService _eventsService;
        public AssureEventExistActionFilter(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idEvent = (long)context.ActionArguments["idEvent"];

            if (_eventsService.GetEventById(idEvent) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}
