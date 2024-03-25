using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_HOSPITAL.Models.Hotel
{
    public class csObjectHotel
    {
        public class requestHotel
        {
            public string hotel_name { get; set; }
            public string hotel_address { get; set; }
            public string hotel_phone { get; set; }
        }
        public class requestHotelUpdate
        {
            public string hotel_name { get; set; }
            public string hotel_address { get; set; }
            public string hotel_phone { get; set; }
            public int hotel_id { get; set; }
        }
        public class requestHotelDelete
        {
            public int hotel_id { get; set; }
        }
        public class responseHotel
        {
            public int status { get; set; }
            public string message { get; set; }
        }
    }
}