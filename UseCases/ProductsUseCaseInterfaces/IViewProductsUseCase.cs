using CoreBusiness;

namespace UseCases.ProductsUseCaseInterfaces
{
    public interface IViewProductsUseCase
    {
        IEnumerable<Product> Execute();
    }
}