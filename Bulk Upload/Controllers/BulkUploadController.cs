using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bulk_Upload.Models;
using ExcelDataReader;
using LumenWorks.Framework.IO.Csv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bulk_Upload.Controllers
{
    public class BulkUploadController : Controller
    {

        static Utilities utilities = new Utilities();
        SqlConnection connection = new SqlConnection(utilities.GetConnection());
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> BulkuploadfileAsync(IFormFile Uploadfile)
        {
            try
            {
                BulkUpload bulkUpload = new BulkUpload();
                // DataTable dataTable = new DataTable();
                //Guid bulkID = new Guid();
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
                //using (StreamReader sr = new StreamReader(path))
                //{
                //    string[] headers = sr.ReadLine().Split(',');
                //    DataTable dt = new DataTable();
                //    foreach (string header in headers)
                //    {
                //        dt.Columns.Add(header);
                //    }
                //    string uniqueUploadId = Guid.NewGuid().ToString();
                //    if (!sr.EndOfStream)
                //    {
                //        while (!sr.EndOfStream)
                //        {
                //            string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                //            DataRow dr = dt.NewRow();
                //            if (rows.Length > 1 && rows[0] != "")
                //            {
                //                for (int i = 0; i < headers.Length; i++)
                //                {
                //                    if (Convert.ToInt32(rows.Length) > i)
                //                    {
                //                        dr[i] = rows[i];
                //                    }
                //                }
                //                dt.Rows.Add(dr);
                //            }
                //        }
                //    }
                //using (var bulkCopy = new SqlBulkCopy(connection))
                //{
                //    bulkCopy.DestinationTableName = "Grace.tbl_Customers";
                //    bulkCopy.ColumnMappings.Add("id", "CustomerID");
                //    bulkCopy.ColumnMappings.Add("Name", "CustomerName");
                //    bulkCopy.ColumnMappings.Add("Country", "CustomerCountry");
                //    bulkCopy.WriteToServer(dt);
                //}
                //using (var filestream = new FileStream(Path.Combine(Uploadfile.FileName), FileMode.Create))
                //{
                //    filestream.Position = 0;
                //    Uploadfile.CopyTo(filestream);
                //    IExcelDataReader excelreader = ExcelReaderFactory.CreateReader(filestream);
                //    var result = excelreader.AsDataSet(new ExcelDataSetConfiguration()
                //    {
                //        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                //        {
                //            UseHeaderRow = true
                //        }
                //    });
                //    excelreader.Close();
                //    dataTable = result.Tables[0];
                //    if (dataTable != null && dataTable.Rows.Count > 0 && result.Tables[0].Rows.Count > 0)
                //    {
                //        dataTable.Columns.Add("BulkID");
                //        for (int i = 0; i < dataTable.Rows.Count; i++)
                //        {
                //            dataTable.Rows[i]["BulkID"] = bulkID.ToString();
                //        }
                //    }
                //}
                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "Deepa.tbl_BulkUpload";
                    bulkCopy.ColumnMappings.Add("EmployeeID", "EmployeeID");
                    bulkCopy.ColumnMappings.Add("Name", "Name");
                    bulkCopy.ColumnMappings.Add("Age", "Age");
                    bulkCopy.ColumnMappings.Add("DOB", "DOB");
                    bulkCopy.ColumnMappings.Add("Email", "Email");
                    bulkCopy.ColumnMappings.Add("Salary", "Salary");
                    bulkCopy.ColumnMappings.Add("Location", "Location");
                    bulkCopy.ColumnMappings.Add("BulkID", "BulkID");
                    //bulkCopy.DestinationTableName = "Deepa.tbl_Members";
                    //bulkCopy.ColumnMappings.Add("MemberID", "MemberID");
                    //bulkCopy.ColumnMappings.Add("Name", "Name");
                    //bulkCopy.ColumnMappings.Add("Email", "Email");
                    //bulkCopy.ColumnMappings.Add("Mobile", "Mobile");
                    //bulkCopy.ColumnMappings.Add("Address", "Address");
                    connection.Open();
                    bulkCopy.WriteToServer(csvTable);
                    connection.Close();
                }
                return Json("");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
