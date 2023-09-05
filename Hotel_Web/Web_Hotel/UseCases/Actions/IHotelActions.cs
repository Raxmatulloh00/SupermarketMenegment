using About;

namespace UseCases.Actions
{
    public interface IHotelActions
    {
        int Add(Hotel hotel);
        void Delete(int hotelid);
        Hotel GetHotelId(int hotelid);
        void Update(Hotel hotel);
        IEnumerable<Hotel> View();
    }
}