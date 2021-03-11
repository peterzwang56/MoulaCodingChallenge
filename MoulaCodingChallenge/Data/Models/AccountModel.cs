using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoulaCodingChallenge.Data.Models
{
    public class AccountModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<PaymentModel> PaymentHistory { get; set; }

    }
}
