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

namespace TransactionHandleAPI.Controllers
{


    public static class ExtensionForService
    {
        //Представление csv файла в виде коллекции
        public static IEnumerable<Transaction> ToAccountingData(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Replace('$', ' ').Replace('.', ',').Split(',');



                yield return new Entities.Transaction
                {
                    TransactionId = int.Parse(columns[0]),
                    Status = columns[1],
                    Type = columns[2],
                    ClientName = columns[3],
                    Amount = decimal.Parse(columns[4])
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
        public TransactionController(TransactionContext context)
        {
            _ctx = context;
        }



        [HttpPost("Upload")]
        public IActionResult UploadCSV([FromBody] string path)
        {
            var query =
         System.IO.File.ReadAllLines(path.Replace("/", @"\"))
               .Skip(1)
               .Where(line => line.Length > 1)
               .ToAccountingData();

            return Ok(query.ToList());

        }
    }







}