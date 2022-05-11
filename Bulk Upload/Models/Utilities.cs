using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bulk_Upload.Models
{
    public class Utilities
    {
        public string GetConnection()
        {
            ConfigurationBuilder configurationbuilder = new ConfigurationBuilder();
            configurationbuilder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false);
            var root = configurationbuilder.Build();
            return root.GetConnectionString("DBconnect");
        }
    }
}
