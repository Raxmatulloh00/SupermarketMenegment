using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.DataStorePulignInterfaces
{
	public interface ITransactionRepository
	{
		public IEnumerable<Transaction> Get(string cashirName);
		public IEnumerable<Transaction> GetByDay(string cashirName, DateTime date);
		public IEnumerable<Transaction> Search(string cashirName, DateTime startDate, DateTime dateTime);
		public void Save(string cashierName, int productId, string productName, double price, int beforeQty, int soldQty);
	}
}
