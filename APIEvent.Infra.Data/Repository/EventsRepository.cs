﻿using APIEvent.Core.Interfaces;
using APIEvent.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace APIEvent.Infra.Data.Repository
{
    public  class EventsRepository : IEventsRepository
    {
        private readonly IConfiguration _configuration;

        public EventsRepository (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CityEvent> GetEvents()
        {
            var query = "SELECT * FROM CityEvent";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query).ToList();
        }

        public CityEvent GetEventById (long idEvent)
        {
            var query = "SELECT * FROM CityEvent where IdEvent = @idEvent";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);
            
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
        }

        public List<CityEvent> GetEventByTitle (string title)
        {
            var query = "SELECT * FROM CityEvent where title LIKE CONCAT('%',@title,'%')";

            var parameters = new DynamicParameters();
            parameters.Add("title", title);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public List<CityEvent> GetEventByLocalDate(string local, DateTime date)
        {
            var query = "SELECT * FROM CityEvent where local = @local AND CONVERT(DATE,dateHourEvent) = @dateHourEvent";

            var parameters = new DynamicParameters();
            parameters.Add("local", local);
            parameters.Add("dateHourEvent", date.Date);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public List<CityEvent> GetEventByRangePriceDate (decimal inicialPrice,decimal finalPrice, DateTime date)
        {
            var query = "SELECT * FROM CityEvent where price BETWEEN @inicialPrice AND @finalPrice AND CONVERT(DATE,dateHourEvent) = @dateHourEvent";

            var parameters = new DynamicParameters();
            parameters.Add("inicialPrice", inicialPrice);
            parameters.Add("finalPrice", finalPrice);
            parameters.Add("dateHourEvent", date.Date);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public bool InsertEvent (CityEvent cityEvent)
        {
            var query = "INSERT INTO CityEvent VALUES (@title, @description, @dateHourEvent, @local, @address, @price, @status)";

            var parameters = new DynamicParameters(cityEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateEvent(long IdEvent,CityEvent cityEvent)
        {
            var query = @"UPDATE CityEvent set title = @title, description = @description, 
                          dateHourEvent = @dateHourEvent,local = @local,address = @address, price = @price , status = @status
                          where idEvent = @idEvent";

            cityEvent.IdEvent = IdEvent;

            var parameters = new DynamicParameters(cityEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteEvent (long idEvent)
        {
            var query = "DELETE FROM CityEvent WHERE idEvent = @idEvent";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateStatus (long idEvent)
        {
            var query = @"UPDATE CityEvent set status = 0 where idEvent = @idEvent";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
    }
}