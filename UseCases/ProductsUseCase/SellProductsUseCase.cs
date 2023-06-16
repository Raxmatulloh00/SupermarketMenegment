using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePulignInterfaces;
using UseCases.ProductsUseCaseInterfaces;
using UseCases.TransactionsUseCaseInterfaces;

namespace UseCases.ProductsUseCase
{
    public class SellProductsUseCase : ISellProductsUseCase
    {
        private readonly IProductRepository productRepository;
        private readonly IRecordTransactionUseCase recordTransactionUseCase;

        public SellProductsUseCase(
            IProductRepository productRepository,
            IRecordTransactionUseCase recordTransactionUseCase)
        {
            this.productRepository = productRepository;
            this.recordTransactionUseCase = recordTransactionUseCase;
        }

        public void Execute(string cashierName, int prductId, int qtyToSell)
        {
            var product = productRepository.GetProductById(prductId);
            if (product == null) return;

            recordTransactionUseCase.Execute(cashierName, prductId, qtyToSell);
            product.Quantity -= qtyToSell;
            productRepository.UpdateProduct(product);
        }
    }
}
