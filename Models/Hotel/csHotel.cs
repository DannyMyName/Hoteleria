using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_HOSPITAL.Models.Hotel
{
    public class csHotel
    {
        public csObjectHotel.responseHotel createHotel(string hotel_name, string hotel_address, string hotel_phone)
        {
            csObjectHotel.responseHotel response = new csObjectHotel.responseHotel();
            // todo: ConfigurationManager es la palabra reservada para acceder a la configuración de la aplicación ( Web.config )
            string connectionString = "";
            SqlConnection sqlConnection = null;
            try
            {
            connectionString = ConfigurationManager.ConnectionStrings["cnConnection"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string query = "INSERT INTO hotel (name, address, phone_number) VALUES (@hotel_name, @hotel_address, @hotel_phone)";
            // todo: Se puede hacer de esta forma, pero es inseguro por inyecciones SQL ⬇️⬇️⬇️⬇️⬇️
            //string query = "INSERT INTO hotel (name, address, phone_number) VALUES " + "( '" + hotel_name + "', '" + hotel_address + "', '" + hotel_phone + "' )";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@hotel_name", hotel_name); // Esto se lo mandamos para evitar inyecciones SQL
            sqlCommand.Parameters.AddWithValue("@hotel_address", hotel_address);
            sqlCommand.Parameters.AddWithValue("@hotel_phone", hotel_phone);
            response.status = sqlCommand.ExecuteNonQuery();
            response.message = "Success";
            }catch(Exception e)
            {
                response.status = 0;
                response.message = "Failed to insert new Hotel: " + e.Message.ToString();
            }
            finally
            {
                sqlConnection.Close();
            }
            return response;
        }
    }
}