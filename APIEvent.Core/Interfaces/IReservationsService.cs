using APIEvent.Core.Models;

namespace APIEvent.Core.Interfaces
{
    public interface IReservationsService
    {
        List<EventReservation> GetRervations();
        EventReservation GetRervationById(long idReservation);
        List<EventReservation> GetRervationByPersonNameTitle(string personName, string title);
        List<EventReservation> GetReservationByIdEvent(long IdEvent);
        bool InsertReservation(EventReservation eventReservation);
        bool UpdateReservation(long idReservation, EventReservation eventReservation);
        bool UpdateReservationQuantity(long idReservation,long quantity);
        bool DeleteReservation(long idReservation);

    }
}
