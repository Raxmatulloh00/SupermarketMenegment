using About;

namespace UseCases.Actions
{
    public interface ICityActions
    {
        void Add(City city);
        void Delete(int cityid);
        City GetCityId(int cityid);
        void Update(City city);
        IEnumerable<City> View();
    }
}