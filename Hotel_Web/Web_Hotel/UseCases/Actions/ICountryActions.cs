using About;

namespace UseCases.Actions
{
    public interface ICountryActions
    {
        void Add(Country city);
        void Delete(int cityid);
        Country GetCountryId(int cityid);
        void Update(Country city);
        IEnumerable<Country> View();
    }
}