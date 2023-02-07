using BMS.Sql.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Sql.Library.Services
{
    public class TransactionService : ModelServiceBase
    {
        public TransactionService(BMSDbContext bmsDbContext) : base(bmsDbContext) { }

        public Transaction Get(int id)
        {
            return BMSDbContext.Transactions.SingleOrDefault(x => x.Id == id);
        }
        public List<Transaction> GetAll(string masterId, DateTimeOffset? lastCreatedDate = null)
        {
            return BMSDbContext.Transactions.ToList();
        }

        public void RemoveTransactionsForChargeController(int chargeControllerId)
        {
            List<Transaction> transactionsFroChargeController = BMSDbContext.Transactions.Where(x => x.ChargeControllerId == chargeControllerId).ToList();
            if (transactionsFroChargeController.Count > 0)
            {
                BMSDbContext.Transactions.RemoveRange(transactionsFroChargeController);
                BMSDbContext.SaveChanges();
            }
        }
        public Tuple<List<Transaction>, List<int>> GetAllTransactionsIdsForChargeControler(int chargeControllerId)
        {
            List<Transaction> transactionsFroChargeController = BMSDbContext.Transactions.Where(x => x.ChargeControllerId == chargeControllerId).ToList();
            List<int> transactionsIds = new List<int>();
            if (transactionsFroChargeController.Count > 0)
            {
                foreach (Transaction transaction in transactionsFroChargeController)
                {
                    transactionsIds.Add(transaction.TransactionId);
                }
            }
            return Tuple.Create(transactionsFroChargeController, transactionsIds);
        }

        public Transaction Save(Transaction transaction)
        {
            try
            {
                if (transaction == null || transaction.Id > 1) return new Transaction();

                Transaction savedTransaction = BMSDbContext.Transactions.Add(transaction).Entity;
                BMSDbContext.SaveChanges();

                return savedTransaction;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Transaction();
            }
        }
    }
}
