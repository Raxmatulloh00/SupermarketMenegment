using About;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStore;

namespace DataStore.SQL
{
    public class ImageRepository
    {
        private readonly Travel travel;

        public ImageRepository(Travel travel)
        {
            this.travel = travel;
        }

        public void AddImage(Image image)
        {
            travel.Image.Add(image);
            travel.SaveChanges();
        }

        public void DeleteImage(int imageid)
        {
            var image = travel.Image.FirstOrDefault(x => x.ImageId == imageid);
            travel.Image.Remove(image);
            travel.SaveChanges();
        }

        public IEnumerable<Image> Get()
        {
            return travel.Image.ToList();
        }

        public List<Image> GetByHotelId(int hotelID)
        {
            var images = from x in travel.Image
                         where x.HotelConnctId == hotelID
                         select x;
            return images.ToList();
        }
    }
}
