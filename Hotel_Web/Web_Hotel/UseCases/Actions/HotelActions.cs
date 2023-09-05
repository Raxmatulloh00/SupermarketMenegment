    using About;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStore;

namespace UseCases.Actions
{
    public class HotelActions : IHotelActions
    {
        private readonly IHotelRepository hotelRepository;

        public HotelActions(IHotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }

        public IEnumerable<Hotel> View()
        {
            return hotelRepository.GetHotel();
        }

        public int Add(Hotel hotel)
        {
            hotelRepository.AddHotel(hotel);
            return hotel.HotelId;
        }

        public void Update(Hotel hotel)
        {
            hotelRepository.UpdateHotel(hotel);
        }

        public Hotel GetHotelId(int hotelid)
        {
            return hotelRepository.GetHotelById(hotelid);
        }

        public void Delete(int hotelid)
        {
            hotelRepository.DeleteHotel(hotelid);
        }
    }
}
