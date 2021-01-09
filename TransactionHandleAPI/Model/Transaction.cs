using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionHandleAPI.Entities
{

    public enum TranStatus
    {
        Pending,
        Completed,
        Cancelled
    }
    public enum TranType
    {
        Refill,
        Withdrawal
    }

    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TransactionId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string ClientName { get; set; }
        public double Amount { get; set; }
    }
}
