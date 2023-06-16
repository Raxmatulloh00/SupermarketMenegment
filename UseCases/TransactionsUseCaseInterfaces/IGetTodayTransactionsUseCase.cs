using CoreBusiness;

namespace UseCases.TransactionsUseCaseInterfaces
{
    public interface IGetTodayTransactionsUseCase
    {
        IEnumerable<Transaction> Execute(string cashirNAme);
    }
}