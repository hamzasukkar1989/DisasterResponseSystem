using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterResponseSystem.Common
{
    public class Enums
    {
        public enum AidType { Financial, Inkind }
        public enum AffectedType { Earthquake, Fire,Theft,Emigration }
        public enum MaritalStatus { Single, Married}
        public enum EvaluatesRequestsStatus { Pending, ForDelivery, Done, Rejected }
    }
}
