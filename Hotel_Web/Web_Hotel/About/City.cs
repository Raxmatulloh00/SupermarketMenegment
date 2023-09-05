using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace About
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        [ForeignKey("Country")]
        [Required]
        public int? CountryConnectId { get; set; }
        [Required]
        public Country? Country { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
    }
}
