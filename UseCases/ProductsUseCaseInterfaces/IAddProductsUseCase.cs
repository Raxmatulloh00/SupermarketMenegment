using CoreBusiness;

namespace UseCases.ProductsUseCaseInterfaces
{
    public interface IAddProductsUseCase
    {
        void Execute(Product product);
    }
}