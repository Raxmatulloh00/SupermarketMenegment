using About;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.DataStore
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetCountry();
        void AddCountry(Country country);
        void UpdateCountry(Country country);
        Country GetCountryById(int countryid);
        void DeleteCountry(int countryid);
    }
}
