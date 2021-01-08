using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransactionHandleAPI.Entities;
using TransactionHandleAPI.Model;
using System.IO;
using System.Net;
using TransactionHandleAPI.Context;
using System.Globalization;
using TransactionHandleAPI.Services;

namespace TransactionHandleAPI.Controllers
{


    public static class ExtensionForService
    {
        //Представление csv файла в виде коллекции
        public static IEnumerable<Transaction> ToAccountingData(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                //var columns = line.Replace('$', ' ').Replace('.', ',').Split(',');


                var columns = line.Replace("$", "").Split(",");

            

                yield return new Transaction
                {
                    TransactionId = int.Parse(columns[0]),
                    Status = columns[1],
                    Type = columns[2],
                    ClientName = columns[3],
                    Amount = double.Parse(columns[4], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo)
                };
            }
        }
    }


    //{"path":"E:/C#/data.csv"}
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionContext _ctx;

        private readonly Services.ITransactionRepository transactionRepository;




        public TransactionController(TransactionContext context, ITransactionRepository transactionRepositroy)
        {
            _ctx = context;
            this.transactionRepository = transactionRepository;
        }




        [HttpPost("Upload")]
        public IActionResult UploadCSV([FromBody] string path)
        {
            var query =
         System.IO.File.ReadAllLines(path.Replace("/", @"\"))
               .Skip(1)
               .Where(line => line.Length > 1)
               .ToAccountingData();


            //transactionRepository.SaveTransaction(query);


            foreach (var item in query)
            {

                var tr = new Transaction
                {
                    Amount = item.Amount,
                    ClientName = item.ClientName,
                    Status = item.Status,
                    TransactionId = item.TransactionId,
                    Type = item.Type
                };






                _ctx.Transactions.Add(tr);
                
                
            }
            _ctx.SaveChanges();

            return Ok(query.ToList());

        }
    }







}