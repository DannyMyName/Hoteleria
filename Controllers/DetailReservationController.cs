using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace API_HOSPITAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailReservationController : ControllerBase
    {
        [HttpGet]
        [Route("reservaciones")]
        public IEnumerable<DetailReservation> Get()
        {
            List<DetailReservation> detailReservations = new List<DetailReservation>();

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GNSKLB4\\SQLEXPRESS01;Initial Catalog=hospitality;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.detail_reservation";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DetailReservation detailReservation = new DetailReservation();
                    detailReservation.IdDetailReservation = Convert.ToInt32(reader["id_detail_reservation"]);
                    detailReservation.InitialDate = Convert.ToDateTime(reader["initial_date"]);
                    detailReservation.FinishDate = Convert.ToDateTime(reader["finish_date"]);
                    detailReservation.TotalDaysReservation = Convert.ToInt32(reader["total_days_reservation"]);
                    detailReservation.IdReservation = Convert.ToInt32(reader["id_reservation"]);
                    detailReservation.IdRoom = Convert.ToInt32(reader["id_room"]);

                    detailReservations.Add(detailReservation);
                }

                reader.Close();
            }

            return detailReservations;
        }

        // GET api/detailreservation/5
        [HttpGet]
        [Route("detailservation")]
        public DetailReservation Get(int id)
        {
            DetailReservation detailReservation = new DetailReservation();

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GNSKLB4\\SQLEXPRESS01;Initial Catalog=hospitality;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.detail_reservation WHERE id_detail_reservation = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    detailReservation.IdDetailReservation = Convert.ToInt32(reader["id_detail_reservation"]);
                    detailReservation.InitialDate = Convert.ToDateTime(reader["initial_date"]);
                    detailReservation.FinishDate = Convert.ToDateTime(reader["finish_date"]);
                    detailReservation.TotalDaysReservation = Convert.ToInt32(reader["total_days_reservation"]);
                    detailReservation.IdReservation = Convert.ToInt32(reader["id_reservation"]);
                    detailReservation.IdRoom = Convert.ToInt32(reader["id_room"]);
                }

                reader.Close();
            }

            return detailReservation;
        }
    }


    public class DetailReservation
    {
        public int IdDetailReservation { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int TotalDaysReservation { get; set; }
        public int IdReservation { get; set; }
        public int IdRoom { get; set; }
    }
}