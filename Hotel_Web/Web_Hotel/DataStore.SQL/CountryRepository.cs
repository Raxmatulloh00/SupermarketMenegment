using System;
using About;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStore;

namespace DataStore.SQL
{
    public class CountryRepository : ICountryRepository
    {
        private readonly Travel travel;

        public CountryRepository(Travel travel)
        {
            this.travel = travel;
        }
             
        public void AddCountry(Country country)
        {
            travel.Country.Add(country);
            travel.SaveChanges();
        }

        public void DeleteCountry(int countryid)
        {
            var country = travel.Country.Find(countryid);

            if (country == null) return;
            travel.Country.Remove(country);
            travel.SaveChanges();
        }

        public IEnumerable<Country> GetCountry()
        {
            return travel.Country.ToList();
        }

        public Country GetCountryById(int countryid)
        {
            return travel.Country.Find(countryid);
        }

        public void UpdateCountry(Country country)
        {
            var count = travel.Country.Find(country.CountryId);
            count.CountryId = country.CountryId;
            count.CountryName = country.CountryName;
            travel.SaveChanges();
        }
    }
}
