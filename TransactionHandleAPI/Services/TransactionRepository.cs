using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TransactionHandleAPI.Context;
using TransactionHandleAPI.Model;

namespace TransactionHandleAPI.Services
{

    //public class TransactionRepository:ITransactionRepository
    //{
    //    private readonly TransactionContext _ctx;
    //    public TransactionRepository(TransactionContext context)
    //    {
    //        _ctx = context;
    //    }

    //    public void SaveTransaction(Transaction transaction)
    //    {
    //        foreach (var item in query)
    //        {

    //            var tr = new Transaction
    //            {
    //                Amount = item.Amount,
    //                ClientName = item.ClientName,
    //                Status = item.Status,
    //                TransactionId = item.TransactionId,
    //                Type = item.Type
    //            };






    //            _ctx.Transactions.Add(tr);

    //        }
    //        _ctx.SaveChanges();
    //    }

    //}
}
