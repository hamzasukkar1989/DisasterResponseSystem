using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterResponseSystem.Models
{
    public class DonorEntities
    {
        public int DonorEntitiesID { get; set; }

        [Required]
        public string Name { get; set; }
        List<FinancialAids> FinancialAids { get; set; }
        List<inkindAids> inkindAids { get; set; }
    }
}
