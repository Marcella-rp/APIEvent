using APIEvent.Core.Interfaces;
using APIEvent.Core.Models;


namespace APIEvent.Core.Services
{
    public class EventService : IEventsService
    {
        public IEventsRepository _eventsRepository;
       
        public EventService(IEventsRepository eventsRepository, IReservationsService reservationsService)
        {
            _eventsRepository = eventsRepository;
        }
        public List<CityEvent> GetEvents()
        {
            return _eventsRepository.GetEvents();
        }
        public CityEvent GetEventById(long idEvent)
        {
           return _eventsRepository.GetEventById(idEvent);
        }
        public List<CityEvent> GetEventByTitle(string title)
        {
            return _eventsRepository.GetEventByTitle(title);
        }
        public List<CityEvent> GetEventByLocalDate(string local, DateTime date)
        {
            return _eventsRepository.GetEventByLocalDate(local, date);
        }
        public List<CityEvent> GetEventByRangePriceDate(decimal inicialPrice, decimal finalPrice, DateTime date)
        {
            return _eventsRepository.GetEventByRangePriceDate(inicialPrice, finalPrice, date);
        }
        public bool InsertEvent(CityEvent cityEvent)
        {
            return _eventsRepository.InsertEvent(cityEvent);
        }
        public bool UpdateEvent(long IdEvent, CityEvent cityEvent)
        {
            return _eventsRepository.UpdateEvent(IdEvent, cityEvent);
        }
        public bool DeleteEvent(long idEvent)
        {
            return _eventsRepository.DeleteEvent(idEvent);
        }
        public bool UpdateStatus(long idEvent)
        {
            return _eventsRepository.UpdateStatus(idEvent);
        }
    }
}
