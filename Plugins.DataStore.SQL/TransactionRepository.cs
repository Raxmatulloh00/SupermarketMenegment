using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePulignInterfaces;

namespace Plugins.DataStore.SQL
{
	public class TransactionRepository : ITransactionRepository
	{
		private readonly MarketContext db;

		public TransactionRepository(MarketContext db)
		{
			this.db = db;
		}

		public IEnumerable<Transaction> Get(string cashirName)
		{
			return db.Transactions.ToList();
		}

		public IEnumerable<Transaction> GetByDay(string cashirName, DateTime date)
		{
			if (string.IsNullOrWhiteSpace(cashirName))
			{
				return db.Transactions.Where(x => x.TimeStamp.Date == date.Date);
			}
			else
			{
				return db.Transactions.Where(x=>
				EF.Functions.Like(x.CashierName, $"%{cashirName}%") &&
				x.TimeStamp.Date == date.Date); 
			}
		}

		public void Save(string cashierName, int productId, string productName, double price, int beforeQty, int soldQty)
		{
			var transaction = new Transaction
			{
				ProductId = productId,
				ProductName = productName,
				TimeStamp = DateTime.Now,
				Price = price,
				BeforeQty = beforeQty,
				SoldQty = soldQty,
				CashierName = cashierName,
			};

			db.Transactions.Add(transaction);
			db.SaveChanges();
		}

		public IEnumerable<Transaction> Search(string cashirName, DateTime startDate, DateTime endDate)
		{
			if (string.IsNullOrWhiteSpace(cashirName))
			{
				return db.Transactions.Where(x => x.TimeStamp >= startDate.Date && x.TimeStamp <= endDate.Date.AddDays(1).Date);
			}
			else
			{
				return db.Transactions.Where(x =>
				EF.Functions.Like(x.CashierName,$"%{cashirName}%") &&
				x.TimeStamp >= startDate.Date && x.TimeStamp <= endDate.Date.AddDays(1).Date); 
			}
		}
	}
}
