using About;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.DataStore
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> GetHotel();
        void AddHotel(Hotel hotel);
        void UpdateHotel(Hotel hotel);
        Hotel GetHotelById(int hotelid);
        IEnumerable<Hotel> GetCityByCountryId(int cityId);

        void DeleteHotel(int hotelid);
    }
}
