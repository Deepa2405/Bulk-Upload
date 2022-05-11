using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bulk_Upload.Models
{
    public class BulkUpload
    {
        public IFormFile Uploadfile { get; set; }
        [Required(ErrorMessage = "Please Choose file")]
        public string File { get; set; }
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public string Location { get; set; }
    }
}
