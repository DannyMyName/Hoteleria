using Antlr.Runtime.Tree;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI.WebControls;

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
            response.message = "Hotel created successfully";
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
        public csObjectHotel.responseHotel updateHotel(string hote_name, string hotel_adddress, string hotel_hone, int hotel_id)
        {
            // 1. Creamos el objeto de la respuesta
            csObjectHotel.responseHotel response = new csObjectHotel.responseHotel();
            // 2. Declaronos nuestras variables para la conexión a la base de datos
            string connectionString = "";
            SqlConnection sqlConnection = null;
            try
            {
                // 3. Creamos nuestra conexión a la base de datos
                connectionString = ConfigurationManager.ConnectionStrings["cnConnection"].ConnectionString;
                sqlConnection = new SqlConnection(connectionString);
                // 4. Abrimos la conexión
                sqlConnection.Open();
                // 5. Creamos nuestra query
                // string query = "UPDATE hotel SET ?  ";
                string query = "UPDATE hotel SET name = @hotel_name, address = @hotel_address, phone_number = @hotel_phone WHERE id_hotel = @id_hotel";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@hotel_name", hote_name);
                sqlCommand.Parameters.AddWithValue("@hotel_address", hotel_adddress);
                sqlCommand.Parameters.AddWithValue("@hotel_phone", hotel_hone);
                sqlCommand.Parameters.AddWithValue("@id_hotel", hotel_id);
                response.status = sqlCommand.ExecuteNonQuery();
                response.message = "Hotel updated successfully";
            }catch(Exception e)
            {
                response.status = 0;
                response.message = "Failed to update Hotel: " + e.Message.ToString();
            }
            finally
            {
                sqlConnection.Close();
            }
            return response;

        }
        public DataSet getHotels()
        {
            string connectionString = "";
            SqlConnection sqlConnection = null;
            DataSet dataSet = new DataSet();
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["cnConnection"].ConnectionString;
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string query = "SELECT * FROM hotel";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataSet);
                dataSet.Tables[0].TableName = "hotels";
            }catch(Exception e)
            {
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }
            return dataSet;
        }
        public DataSet getHotelById(int hotel_id)
        {
            string connectionString;
            SqlConnection sqlConnection = null;
            DataSet dataSet = new DataSet();
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["cnConnection"].ConnectionString;
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string query = "SELECT * FROM hotel WHERE id_hotel = @hotel_id";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@hotel_id", hotel_id);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataSet);
                dataSet.Tables[0].TableName = "hotels";
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }
            return dataSet;
        }
        public csObjectHotel.responseHotel deleteHotel (int id_hotel)
        {
            csObjectHotel.responseHotel response = new csObjectHotel.responseHotel();
            string connectionString = "";
            SqlConnection sqlConnection = null;
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["cnConnection"].ConnectionString;
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string query = "DELETE FROM hotel WHERE id_hotel = @id_hotel";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id_hotel", id_hotel);
                response.status = sqlCommand.ExecuteNonQuery();
                response.message = "Hotel deleted successfully";
            }catch(Exception e)
            {
                response.status = 0;
                response.message = "Failed to delete Hotel: " + e.Message.ToString();
            }
            finally
            {
                sqlConnection.Close();
            }
            return response;
        }

    }
}