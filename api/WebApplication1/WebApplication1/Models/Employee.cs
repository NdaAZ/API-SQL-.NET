using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Employee
    {
        public string Asset_id { get; set; }
        public string Asset_Name { get; set; }
        public string Asset_Category { get; set; }

        public string Model { get; set; }

        public string Department { get; set; }
        public string Asset_Floor { get; set; }
        public string X_axis { get; set; }
        public string Y_axis { get; set; }

        public string Warranty_Exp { get; set; }

        public string PhotoFileName { get; set; }
    }
}
