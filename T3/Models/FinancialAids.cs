using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterResponseSystem.Models
{
    public class FinancialAids
    {
        public int FinancialAidsID { get; set; }
        public double Amount { get; set; }
        public DonorEntities DonorEntitie { get; set; }

    }
}
