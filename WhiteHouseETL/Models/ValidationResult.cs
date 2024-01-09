using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteHouseETL.Models
{
    public class ValidationResult
    {
        public WhiteHouseStaff Record { get; set; } = new WhiteHouseStaff();
        public Employee Employee { get; set; }
        public Position Position { get; set; }
        public Salary Salary { get; set; }
        public bool Passed { get; set; }
        public Dictionary<string, string> Results { get; set; } = new Dictionary<string, string>();
    }
}
