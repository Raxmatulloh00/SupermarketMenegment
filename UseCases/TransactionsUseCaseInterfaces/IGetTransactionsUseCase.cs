using CoreBusiness;

namespace UseCases.TransactionsUseCaseInterfaces
{
    public interface IGetTransactionsUseCase
    {
        IEnumerable<Transaction> Execute(string cashierName, DateTime startDate, DateTime endDate);
    }
}