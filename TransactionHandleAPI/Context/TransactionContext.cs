using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionHandleAPI.Entities;
using TransactionHandleAPI.Model;

namespace TransactionHandleAPI.Context
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        {

        }

        //public DbSet<TransactionTest> Transactions { get; set; }
        public DbSet<Transaction> Transactions { get; set; } 


    }
}
