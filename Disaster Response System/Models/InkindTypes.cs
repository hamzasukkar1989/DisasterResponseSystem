using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterResponseSystem.Models
{
    public class InkindTypes
    {
        public int ID { get; set; }
        public string InkindName { get; set; }
        [Required]
        public int QTY { get; set; }
    }
}
