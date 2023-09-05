using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace About
{
    public class Image
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
            
        [ForeignKey("Hotel")]
        public int HotelConnctId { get; set; }
        public Hotel Hotel { get; set; }
        public string ImageName { get; set; }
    }
}
