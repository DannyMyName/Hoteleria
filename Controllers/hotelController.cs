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
    }
}