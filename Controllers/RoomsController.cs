using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        [HttpGet]
        [Route("rooms")]
        public IEnumerable<Room> Get()
        {
            List<Room> rooms = new List<Room>();

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GNSKLB4\\SQLEXPRESS01;Initial Catalog=hospitality;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.room";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Room room = new Room();
                    room.IdRoom = Convert.ToInt32(reader["id_room"]);
                    room.Price = Convert.ToDecimal(reader["price"]);
                    room.Reserve = Convert.ToBoolean(reader["reserve"]);
                    room.IdHotel = Convert.ToInt32(reader["id_hotel"]);
                    room.IdTypeRoom = Convert.ToInt32(reader["id_type_room"]);

                    rooms.Add(room);
                }

                reader.Close();
            }

            return rooms;
        }

        [HttpGet]
        [Route("room")]
        public Room Get(int id)
        {
            Room room = new Room();

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GNSKLB4\\SQLEXPRESS01;Initial Catalog=hospitality;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.room WHERE id_room = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    room.IdRoom = Convert.ToInt32(reader["id_room"]);
                    room.Price = Convert.ToDecimal(reader["price"]);
                    room.Reserve = Convert.ToBoolean(reader["reserve"]);
                    room.IdHotel = Convert.ToInt32(reader["id_hotel"]);
                    room.IdTypeRoom = Convert.ToInt32(reader["id_type_room"]);
                }

                reader.Close();
            }

            return room;
        }
    }

    public class Room
    {
        public int IdRoom { get; set; }
        public decimal Price { get; set; }
        public bool Reserve { get; set; }
        public int IdHotel { get; set; }
        public int IdTypeRoom { get; set; }
    }
}
