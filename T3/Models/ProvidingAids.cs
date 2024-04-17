using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DisasterResponseSystem.Common.Enums;

namespace DisasterResponseSystem.Models
{
    public class ProvidingAids
    {
        public int ID { get; set; }
        public List<AffectedIndividualsRequests> AffectedIndividualsRequests { get; set; }
    }
}
