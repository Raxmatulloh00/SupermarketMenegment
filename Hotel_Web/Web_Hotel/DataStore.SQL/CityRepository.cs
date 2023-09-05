using About;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStore;

namespace DataStore.SQL
{
    public class CityRepository : ICityRepository
    {
        private readonly Travel travel;
        public CityRepository(Travel travel)
        {
            this.travel = travel;
        }

        public void AddCity(City city)
        {
            travel.Citiy.Add(city);
            travel.SaveChanges();
        }

        public void DeleteCity(int cityid)


        {
            var cit = travel.Citiy.Find(cityid);
            if (cit == null) return;

            travel.Citiy.Remove(cit);   
            travel.SaveChanges();
        }

        public IEnumerable<City> GetCities()
        {
            return travel.Citiy.ToList();
        }

        public IEnumerable<City> GetCityByCountryId(int countryId)
        {
           return travel.Citiy.Where(w => w.CountryConnectId == countryId).ToList();
        }

        public City GetCityById(int cityid)
        {
            return travel.Citiy.Find(cityid);       

        }

        public void UpdateCity(City city)
        {
            var cit = travel.Citiy.Find(city.CityId);
            cit.CityId =city.CityId;
            cit.CityName = city.CityName;
            cit.CountryConnectId = city.CountryConnectId;

            travel.SaveChanges();
        }       
    }
}
