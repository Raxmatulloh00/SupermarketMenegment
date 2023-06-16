using CoreBusiness;

namespace UseCases.UseCaseInterfaces
{
    public interface IViewCategoriesUseCase
    {
        IEnumerable<Category> Execute();
        bool Something(int id);
        int Qoshish (int sum1,  int sum2);
    }
}