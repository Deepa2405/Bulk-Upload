using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bulk_Upload.Models
{
    public class BaseClass
    {
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
