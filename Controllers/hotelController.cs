using API_HOSPITAL.Models.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Http;
using static API_HOSPITAL.Models.Hotel.csObjectHotel;

namespace API_HOSPITAL.Controllers
{
    public class hotelController : ApiController
    {
        // localhost:5000/rest/api/hotel
        [HttpPost]
        [Route("rest/api/createHotel")]
        public IHttpActionResult createHotel (requestHotel model)
        {
            return Ok(new csHotel().createHotel(model.hotel_name, model.hotel_address, model.hotel_phone));
        }
        [HttpPost]
        [Route("rest/api/updateHotel")]
        public IHttpActionResult updateHotel(requestHotelUpdate model)
        {
            return Ok(new csHotel().updateHotel(model.hotel_name, model.hotel_address, model.hotel_phone, model.hotel_id));
        }
        [HttpPost]
        [Route("rest/api/deleteHotel")]
        public IHttpActionResult deleteHotel(requestHotelDelete model)
        {
            return Ok(new csHotel().deleteHotel(model.hotel_id));
        }
        [HttpGet]
        [Route("rest/api/listHotels")]
        public IHttpActionResult listHotels()
        {
            return Ok(new csHotel().getHotels());
        }
        [HttpGet]
        [Route("rest/api/getHotelById")]
        public IHttpActionResult getHotelById(int hotel_id)
        {
            return Ok(new csHotel().getHotelById(hotel_id));
        }
    }
}