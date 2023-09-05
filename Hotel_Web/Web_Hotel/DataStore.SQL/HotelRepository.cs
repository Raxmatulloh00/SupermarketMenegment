using About;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStore;

namespace DataStore.SQL
{
    public class HotelRepository : IHotelRepository 
    {
        private readonly Travel travel;

     
        public HotelRepository(Travel travel)
        {
            this.travel = travel;
        }

        public void AddHotel(Hotel hotel)
        {
            travel.Hotel.Add(hotel);
            travel.SaveChanges();
        }

        public void DeleteHotel(int hotelid)
        {
            var hot = travel.Hotel.Find(hotelid);
            if (hot == null) return;    

            travel.Hotel.Remove(hot); 
            travel.SaveChanges();
        }

        public IEnumerable<Hotel> GetCityByCountryId(int cityId)
        {
            return  travel.Hotel.Where(w => w.CityConnctId == cityId).ToList();
        }

        public IEnumerable<Hotel> GetHotel()
        {
            Console.WriteLine(travel);
            return travel.Hotel.ToList();
        }

        public Hotel GetHotelById(int hotelid)
        {
            return travel.Hotel.Find(hotelid);

        }

        public void UpdateHotel(Hotel hotel)
        {
            var hot = travel.Hotel.Find(hotel.HotelId);
            hot.HotelId = hotel.HotelId;
            hot.CityConnctId = hotel.CityConnctId;
            hot.HotelName = hotel.HotelName;    
            hot.HotelPrice = hotel.HotelPrice;
            hot.HotelStar = hotel.HotelStar;      
            hot.HotelDescription = hotel.HotelDescription; 

            travel.SaveChanges();
        }
    }
}
