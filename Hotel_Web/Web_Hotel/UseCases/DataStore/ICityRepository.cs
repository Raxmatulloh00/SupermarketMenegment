using About;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.DataStore
{
    public interface ICityRepository
    {
        IEnumerable<City> GetCities();
        void AddCity(City city);
        void UpdateCity(City city);
        City GetCityById(int cityid);
        IEnumerable<City> GetCityByCountryId(int countryId);
        void DeleteCity(int cityid);
    }
}
