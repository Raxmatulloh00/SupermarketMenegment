namespace UseCases.ProductsUseCaseInterfaces
{
    public interface ISellProductsUseCase
    {
        void Execute(string cashierName, int prductId, int qtyToSell);
    }
}