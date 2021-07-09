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
using Microsoft.EntityFrameworkCore;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;


namespace TransactionHandleAPI.Controllers
{


    public static class ExtensionForService
    {
        //Представление csv файла в виде коллекции
        public static IEnumerable<Transaction> ToAccountingData(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
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


    //{"path":"E:/C#/data.csv"} "E:/C#/data.csv"
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionContext _ctx;


        public TransactionController(TransactionContext context)
        {
            _ctx = context;
        }

        [HttpPost("Upload")]
        public IActionResult UploadCSV([FromBody] string path)
        {
            var rawData =
           System.IO.File.ReadAllLines(path.Replace("/", @"\"));

            //if rawData is null or Empty
            if (rawData== null || !rawData.Any())
            {
               throw new Exception("Transaction is empty");                
            }
            else
            {
              var transactions =  rawData.Skip(1)
               .Where(line => line.Length > 1)
               .ToAccountingData();

                foreach (var item in transactions)
                {
                    if (_ctx.Transactions.Contains(item))
                    {
                        _ctx.Transactions.Where(x => x.TransactionId == item.TransactionId);
                        _ctx.Transactions.Update(item);
                    }
                    else
                    {
                        _ctx.Transactions.Add(item);                      
                    }
                }
                _ctx.SaveChanges();
                return Ok(transactions.ToList());
            }                
        }

        [HttpGet("Get")]
        public IActionResult ShowAllTran()
        {
            
          var listOfTran = _ctx.Transactions.OrderBy(x => x.TransactionId).ToList();

            return Ok(listOfTran);

        }


        //api/transaction/Edit?id=1&status=1
        [HttpPost("Edit")]
        public IActionResult EditTran(int id, TranStatus status)
        {
           var findTran = _ctx.Transactions.Where(x => x.TransactionId == id);

            var z = status.ToString();
            foreach (var item in findTran)
            {
                item.Status = z;
                _ctx.Transactions.Update(item);
            }

            _ctx.SaveChanges();

            return Ok();
           
        }




        //[HttpGet("Export")]
        //public IActionResult ExportToExcel([FromBody] int[] parameters)
        //{




        //    return Ok(query.ToList());

        //}


        ///new feature 


        [HttpGet("Delete")]
        public IActionResult ExportToExcel([FromBody] int[] parameters)
        {

            //dnn


            return Ok(query.ToList());

        }



    }







}