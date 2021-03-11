using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoulaCodingChallenge.Data.Models
{
    public class PaymentModel
    {
        [Key]
        public long PaymentId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string ClosedReason { get; set; }
        [ForeignKey("AccountModel")]
        public int AccountId { get; set; }
        public AccountModel Account { get; set; }
    }

}
