using APIEvent.Core.Interfaces;
using APIEvent.Core.Models;


namespace APIEvent.Core.Services
{
    public class ReservationService : IReservationsService
    {
        public IReservationsRepository _reservationsRepository;
        public ReservationService(IReservationsRepository reservationsRepository)
        {
            _reservationsRepository = reservationsRepository;
        }
        public List<EventReservation> GetRervations()
        {
            return _reservationsRepository.GetRervations();
        }
        public EventReservation GetRervationById(long idReservation)
        {
            return _reservationsRepository.GetRervationById(idReservation);
        }
        public List<EventReservation> GetRervationByPersonNameTitle(string personName, string title)
        {
            return _reservationsRepository.GetRervationByPersonNameTitle(personName, title);
        }
        public List<EventReservation> GetReservationByIdEvent(long IdEvent)
        {
            return _reservationsRepository.GetReservationByIdEvent(IdEvent);
        }
        public bool InsertReservation(EventReservation eventReservation)
        {
            return _reservationsRepository.InsertReservation(eventReservation);
        }
        public bool UpdateReservation(long idReservation, EventReservation eventReservation)
        {
            return _reservationsRepository.UpdateReservation(idReservation,eventReservation);
        }
        public bool UpdateReservationQuantity(long idReservation,long quantity)
        {
            return _reservationsRepository.UpdateReservationQuantity(idReservation,quantity);
        }
        public bool DeleteReservation(long idReservation)
        {
            return _reservationsRepository.DeleteReservation(idReservation);
        }
    }
}
