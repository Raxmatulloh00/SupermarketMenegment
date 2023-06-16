using CoreBusiness;

namespace UseCases.ProductsUseCaseInterfaces
{
    public interface IViewProductsByCategoryId
    {
        IEnumerable<Product> Execute(int categoryId);
    }
}