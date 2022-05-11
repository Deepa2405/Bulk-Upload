using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bulk_Upload.Models;
using LumenWorks.Framework.IO.Csv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bulk_Upload.Controllers
{
    public class BulkfileController : Controller
    {

        static Utilities utilities = new Utilities();
        SqlConnection connection = new SqlConnection(utilities.GetConnection());
        public async Task<JsonResult> CreatefileAsync(IFormFile Uploadfile)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", Uploadfile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Uploadfile.CopyToAsync(stream);
                }
                var csvTable = new DataTable();
                using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(path)), true))
                {
                    csvTable.Load(csvReader);
                }
                using (var bulkCopy = new SqlBulkCopy(connection))
                {

                    bulkCopy.DestinationTableName = "Deepa.tbl_Members";
                    bulkCopy.ColumnMappings.Add("MemberID", "MemberID");
                    bulkCopy.ColumnMappings.Add("Name", "Name");
                    bulkCopy.ColumnMappings.Add("Email", "Email");
                    bulkCopy.ColumnMappings.Add("Mobile", "Mobile");
                    bulkCopy.ColumnMappings.Add("Address", "Address");
                    connection.Open();
                    bulkCopy.WriteToServer(csvTable);
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json("");
        }
    }
}