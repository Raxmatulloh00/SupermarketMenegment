using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace About
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public int HotelStar { get; set; }
        public int HotelPrice { get; set; }
        public string HotelDescription { get; set; }
        [ForeignKey("City")]
        public int CityConnctId { get; set; }
        public City City { get; set; }

        public ICollection<Image> Images { get; set; }

    }
}
