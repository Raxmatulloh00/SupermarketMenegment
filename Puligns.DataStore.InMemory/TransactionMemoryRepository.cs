﻿using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePulignInterfaces;

namespace Puligns.DataStore.InMemory
{
	public class TransactionMemoryRepository : ITransactionRepository
	{
		private List<Transaction> transactions;

		public TransactionMemoryRepository()
		{
			transactions = new List<Transaction>();
		}

		public IEnumerable<Transaction> Get(string cashirName)
		{
			if (string.IsNullOrWhiteSpace(cashirName))
			{
				return transactions;
			}
			else
			{
				return transactions.Where(x => string.Equals(x.CashierName,cashirName, StringComparison.OrdinalIgnoreCase));
			}
		}

		public IEnumerable<Transaction> GetByDay(string cashirName, DateTime date)
		{
			if (string.IsNullOrWhiteSpace(cashirName))
			{
				return transactions.Where(x=> x.TimeStamp.Date == date.Date);		
			}
			else
			{
				return transactions.Where(x => 
				string.Equals(x.CashierName, cashirName, StringComparison.OrdinalIgnoreCase) &&
				x.TimeStamp.Date == date.Date);
			}
		}

		public void Save(string cashierName, int productId, string productName, double price, int beforeQty, int soldQty)
		{
			int transactionId = 0;
			if (transactions != null && transactions.Count > 0)
			{
				int maxId = transactions.Max(x => x.TransactionId);
				transactionId = maxId + 1;
			}
			else
			{
				transactionId = 1;	
			}
				
			transactions.Add(new Transaction
			{ 
				TransactionId = transactionId,
				ProductId = productId,
				ProductName = productName,
				TimeStamp = DateTime.Now,
				Price = price,
				BeforeQty = beforeQty,
				SoldQty = soldQty,
				CashierName = cashierName, 
			});
		}

		public IEnumerable<Transaction> Search(string cashirName, DateTime startDate, DateTime endDate)
		{
			if (string.IsNullOrWhiteSpace(cashirName))
			{
				return transactions.Where(x => x.TimeStamp >= startDate.Date && x.TimeStamp <= endDate.Date.Date.AddDays(1).Date);
			}
			else
			{
				return transactions.Where(x =>
				string.Equals(x.CashierName, cashirName, StringComparison.OrdinalIgnoreCase) &&
				x.TimeStamp >= startDate.Date && x.TimeStamp <= endDate.Date.Date.AddDays(1).Date);
			}
		}
	}
}
