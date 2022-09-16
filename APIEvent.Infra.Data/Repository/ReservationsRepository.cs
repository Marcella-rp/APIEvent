using APIEvent.Core.Interfaces;
using APIEvent.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace APIEvent.Infra.Data.Repository
{
    public class ReservationsRepository : IReservationsRepository
    {
        private readonly IConfiguration _configuration;

        public ReservationsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<EventReservation> GetRervations()
        {
            var query = "SELECT * FROM EventReservation";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<EventReservation>(query).ToList();
        }

        public EventReservation GetRervationById(long idReservation)
        {
            var query = "SELECT * FROM EventReservation where idReservation = @idReservation";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<EventReservation>(query, parameters);
        }

        public List<EventReservation> GetRervationByPersonNameTitle(string personName, string title)
        {
            var query = "SELECT * FROM EventReservation AS ev INNER JOIN CityEvent AS ci ON ev.IdEvent = ci.IdEvent WHERE ev.PersonName = @personName AND ci.Title LIKE CONCAT('%',@title,'%')";

            var parameters = new DynamicParameters();
            parameters.Add("personName", personName);
            parameters.Add("title", title);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<EventReservation>(query, parameters).ToList();
        }

        public List<EventReservation> GetReservationByIdEvent (long idEvent)
        {
            var query = "SELECT * FROM EventReservation WHERE IdEvent = @idEvent";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<EventReservation>(query, parameters).ToList();
        }

        public bool InsertReservation(EventReservation eventReservation)
        {
            var query = "INSERT INTO EventReservation VALUES (@idEvent, @personName, @quantity)";

            var parameters = new DynamicParameters(eventReservation);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateReservation(long idReservation,EventReservation eventReservation)
        {
            var query = @"UPDATE EventReservation set idEvent = @idEvent, personName = @personName, quantity = @quantity
                          where idReservation = @idReservation";

            eventReservation.IdReservation = idReservation;
            var parameters = new DynamicParameters(eventReservation);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateReservationQuantity(long idReservation, long quantity)
        {
            var query = @"UPDATE EventReservation set quantity = @quantity
                          where idReservation = @idReservation";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);
            parameters.Add("quantity", quantity);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteReservation (long idReservation)
        {
            var query = "DELETE FROM EventReservation WHERE idReservation = @idReservation";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
    }
}
