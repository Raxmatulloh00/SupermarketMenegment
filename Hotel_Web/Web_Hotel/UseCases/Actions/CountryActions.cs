using About;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStore;

namespace UseCases.Actions
{
    public class CountryActions : ICountryActions
    {
        private readonly ICountryRepository countryRepository;

        public CountryActions(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public IEnumerable<Country> View()
        {
            return countryRepository.GetCountry();
        }

        public void Add(Country city)
        {
            countryRepository.AddCountry(city);
        }

        public void Update(Country city)
        {
            countryRepository.UpdateCountry(city);
        }

        public Country GetCountryId(int cityid)
        {
            return countryRepository.GetCountryById(cityid);
        }

        public void Delete(int cityid)
        {
            countryRepository.DeleteCountry(cityid);
        }
    }
}
