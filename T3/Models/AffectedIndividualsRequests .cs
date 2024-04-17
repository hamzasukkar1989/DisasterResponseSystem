using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static DisasterResponseSystem.Common.Enums;

namespace DisasterResponseSystem.Models
{
    public class AffectedIndividualsRequests
    {
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int NationalNumber  { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public int FamilyMembers { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public double Evaluation { get; set; }
        public DateTime RequestDate { get; set; }
        public AffectedType AffectedType { get; set; }
        public EvaluatesRequestsStatus EvaluatesRequestsStatus { get; set; }
        public double AmountSpent { get; set; } = 0;
        public List<AffectedIndividualsInkind> AffectedIndividualsInkinds { get; set; }
      
    }
    
}
