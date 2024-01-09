using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteHouseETL.Models
{
    public class StatusEnum
    {
        public enum Status
        {
            [Description("Employee")]
            Employee,
            [Description("Detailee")]
            Detailee,
            [Description("Employee (part-time)")]
            PartTime
        }
    }
}
