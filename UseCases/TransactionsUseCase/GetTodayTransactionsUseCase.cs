using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePulignInterfaces;
using UseCases.TransactionsUseCaseInterfaces;

namespace UseCases.TransactionsUseCase
{
    public class GetTodayTransactionsUseCase : IGetTodayTransactionsUseCase
    {
        private readonly ITransactionRepository transactionRepository;

        public GetTodayTransactionsUseCase(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public IEnumerable<Transaction> Execute(string cashirNAme)
        {
            return transactionRepository.GetByDay(cashirNAme, DateTime.Now);
        }
    }
}
