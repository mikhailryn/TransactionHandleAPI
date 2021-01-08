using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionHandleAPI.Context;
using TransactionHandleAPI.Model;

namespace TransactionHandleAPI.Services
{

    public class TransactionRepository:ITransactionRepository
    {
        private readonly TransactionContext _ctx;
        public TransactionRepository(TransactionContext context)
        {
            _ctx = context;
        }

        public void AddTransaction()
        {
            
        }

    }
}
