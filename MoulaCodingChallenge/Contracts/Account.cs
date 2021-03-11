using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoulaCodingChallenge.Contracts
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<Payment> PaymentHistory { get; set; }

    }
}
