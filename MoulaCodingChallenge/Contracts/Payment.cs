using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoulaCodingChallenge.Contracts
{
    public class Payment
    {
        public long PaymentId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string ClosedReason { get; set; }
    }
}
