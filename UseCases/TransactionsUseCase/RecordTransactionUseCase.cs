using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UseCases.DataStorePulignInterfaces;
using UseCases.ProductsUseCaseInterfaces;
using UseCases.TransactionsUseCaseInterfaces;

namespace UseCases.TransactionsUseCase
{
    public class RecordTransactionUseCase : IRecordTransactionUseCase
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IGetProductsByIdUseCase getProductsByIdUseCase;

        public RecordTransactionUseCase(
            ITransactionRepository transactionRepository,
            IGetProductsByIdUseCase getProductsByIdUseCase
            )
        {
            this.transactionRepository = transactionRepository;
            this.getProductsByIdUseCase = getProductsByIdUseCase;
        }

        public void Execute(string cashierName, int productId, int qty)
        {
            var product = getProductsByIdUseCase.Execute(productId);
            transactionRepository.Save(cashierName, productId, product.Name, product.Price.Value, product.Quantity.Value, qty);
        }
    }
}
