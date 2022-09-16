using APIEvent.Core.Models;


namespace APIEvent.Core.Interfaces
{
    public  interface IEventsRepository 
    {
        List<CityEvent> GetEvents();
        CityEvent GetEventById(long idEvent);
        List<CityEvent> GetEventByTitle(string title);
        List<CityEvent> GetEventByLocalDate(string local, DateTime date);
        List<CityEvent> GetEventByRangePriceDate(decimal inicialPrice, decimal finalPrice, DateTime date);
        bool InsertEvent(CityEvent cityEvent);
        bool UpdateEvent(long IdEvent, CityEvent cityEvent);
        bool DeleteEvent(long idEvent);
        bool UpdateStatus(long idEvent);
    }
}
