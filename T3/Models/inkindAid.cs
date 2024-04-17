using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterResponseSystem.Models
{
    public class inkindAids
    {
        public int inkindAidsID { get; set; }

        [Required]
        public int QTY { get; set; }

        public InkindTypes InkindType { get; set; }

        public DonorEntities DonorEntitie { get; set; }
    }
}
