using CoreBusiness;

namespace UseCases.ProductsUseCaseInterfaces
{
    public interface IGetProductsByIdUseCase
    {
        Product Execute(int productId);
    }
}