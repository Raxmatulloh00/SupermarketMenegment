using About;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStore;

namespace UseCases.Actions
{
    public class CityActions : ICityActions
    {
        private readonly ICityRepository cityRepository;

        public CityActions(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public IEnumerable<City> View()
        {
            return cityRepository.GetCities();
        }

        public void Add(City city)
        {
            cityRepository.AddCity(city);
        }

        public void Update(City city)
        {
            cityRepository.UpdateCity(city);
        }

        public City GetCityId(int cityid)
        {
            return cityRepository.GetCityById(cityid);
        }

        public void Delete(int cityid)
        {
            cityRepository.DeleteCity(cityid);
        }
    }
}
