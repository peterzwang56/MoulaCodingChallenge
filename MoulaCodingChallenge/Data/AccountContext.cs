using Microsoft.EntityFrameworkCore;
using MoulaCodingChallenge.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoulaCodingChallenge.Data
{

    /// <summary>
    /// EF DB context
    /// </summary>
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {

        }

        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<PaymentModel> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>().ToTable("Account");
            modelBuilder.Entity<PaymentModel>().ToTable("Payment");


            // seeding data
            modelBuilder.Entity<AccountModel>().HasData(new AccountModel() { Id = 1, UserName = "Eren", AccountNumber = 1, Balance = 104.11M });
            modelBuilder.Entity<AccountModel>().HasData(new AccountModel() { Id = 2, UserName = "Eren", AccountNumber = 2, Balance = 104.4M });
            modelBuilder.Entity<AccountModel>().HasData(new AccountModel() { Id = 3, UserName = "Mikasa", AccountNumber = 3, Balance = 104.1M });
            int i = 1;
            foreach (var p1 in GeneratePayments(1))
            {
                p1.PaymentId = i;
                i++;
                modelBuilder.Entity<PaymentModel>().HasData(p1);
            }
            foreach (var p2 in GeneratePayments(2))
            {
                p2.PaymentId = i;
                i++;
                modelBuilder.Entity<PaymentModel>().HasData(p2);
            }
        }


        private IEnumerable<PaymentModel> GeneratePayments(int accountid)
        {
            List<PaymentModel> payments = new List<PaymentModel>();

            DateTime from = DateTime.UtcNow.AddMinutes(-10);

            for (int i = 0; i < 4; i++)
            {
                payments.Add(new PaymentModel() { Amount = i + 10.33M, Status = "Open", Date = from.AddMinutes(i),AccountId = accountid});
            }
            
            return payments;


        }
    }
}
