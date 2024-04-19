using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace API_HOSPITAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        [HttpGet]
        [Route("reservations")]
        public IEnumerable<Reservation> Get()
        {
            List<Reservation> reservations = new List<Reservation>();

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GNSKLB4\\SQLEXPRESS01;Initial Catalog=hospitality;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.reservation";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Reservation reservation = new Reservation();
                    reservation.IdReservation = Convert.ToInt32(reader["id_reservation"]);
                    reservation.Total = Convert.ToDecimal(reader["total"]);
                    reservation.EmitionDate = Convert.ToDateTime(reader["emition_date"]);
                    reservation.IdClient = Convert.ToInt32(reader["id_client"]);

                    reservations.Add(reservation);
                }

                reader.Close();
            }

            return reservations;
        }

        [HttpGet]
        [Route("reservation")]
        public Reservation Get(int id)
        {
            Reservation reservation = new Reservation();

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GNSKLB4\\SQLEXPRESS01;Initial Catalog=hospitality;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.reservation WHERE id_reservation = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    reservation.IdReservation = Convert.ToInt32(reader["id_reservation"]);
                    reservation.Total = Convert.ToDecimal(reader["total"]);
                    reservation.EmitionDate = Convert.ToDateTime(reader["emition_date"]);
                    reservation.IdClient = Convert.ToInt32(reader["id_client"]);
                }

                reader.Close();
            }

            return reservation;
        }
    }

    public class Reservation
    {
        public int IdReservation { get; set; }
        public decimal Total { get; set; }
        public DateTime EmitionDate { get; set; }
        public int IdClient { get; set; }
    }
}
