using System.ComponentModel.DataAnnotations;

namespace About
{
    public class Country
    {
        public int CountryId { get; set; }
        [Required]
        public string CountryName { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}