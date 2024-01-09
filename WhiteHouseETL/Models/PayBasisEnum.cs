using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteHouseETL.Models
{
    public class PayBasisEnum
    {
        public enum PayBasis
        {
            [Description("Per Diem")]
            PerDiem,
            [Description("Per Annum")]
            PerAnnum
        }
    }
}
