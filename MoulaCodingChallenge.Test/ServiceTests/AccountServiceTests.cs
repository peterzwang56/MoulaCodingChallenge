using Microsoft.EntityFrameworkCore;
using MoulaCodingChallenge.Contracts;
using MoulaCodingChallenge.Data;
using MoulaCodingChallenge.Data.Models;
using MoulaCodingChallenge.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoulaCodingChallenge.Test.ServiceTests
{
    public class AccountServiceTests
    {
        private AccountContext _dbcontext;
        private IEnumerable<PaymentModel> _expectedPaymentHistory;

        private const string VALID_USER_WITH_HISTORY = "Armin";
        private const int VALID_ACCOUNT_WITH_HISTORY = 1;

        private const string VALID_USER_WITHOUT_HISTORY = "Annie";
        private const int VALID_ACCOUNT_WITHOUT_HISTORY = 2;

        private const string INVALID_USER = "Bertolt";
        private const int INVALID_ACCOUNT = 4;

        [OneTimeSetUp]
        public void Setup()
        {

            var options = new DbContextOptionsBuilder<AccountContext>()
                                    .UseInMemoryDatabase(databaseName: "Moula")
                                    .Options;
            _expectedPaymentHistory = GeneratePayments();
            // Insert seed data into the database using one instance of the context
            _dbcontext = new AccountContext(options);
            _dbcontext.Accounts.Add(new AccountModel { AccountNumber = VALID_ACCOUNT_WITH_HISTORY, Balance = 100, PaymentHistory = _expectedPaymentHistory, UserName = VALID_USER_WITH_HISTORY });
            _dbcontext.Accounts.Add(new AccountModel { AccountNumber = VALID_ACCOUNT_WITHOUT_HISTORY, Balance = 100, UserName = VALID_USER_WITHOUT_HISTORY });

            _dbcontext.SaveChanges();

        }

        /// <summary>
        /// GIVEN a valid user 
        /// AND account # is not belonging to the user
        /// WHEN requesting account details
        /// THEN it should return null
        /// </summary>
        [Test]
        public void ValidUserWithoutAnAccountShouldReturnNull()
        {
            var _repo = new AccountRepository(_dbcontext);
            AccountService service = new AccountService(_repo);
            var account = service.GetAccountAsync(VALID_ACCOUNT_WITHOUT_HISTORY, VALID_USER_WITH_HISTORY).Result;
            Assert.IsNull(account);
        }

        /// <summary>
        /// GIVEN a user that does not exist in DB
        /// AND Account # is a valid one
        /// WHEN requesting account details
        /// THEN it should return null
        /// </summary>
        [Test]
        public void InvalidUserwithValidAccountShouldReturnNull()
        {
            var _repo = new AccountRepository(_dbcontext);
            AccountService service = new AccountService(_repo);
            var account = service.GetAccountAsync(VALID_ACCOUNT_WITH_HISTORY, INVALID_USER).Result;
            Assert.IsNull(account);
        }

        /// <summary>
        /// GIVEN a user that does not exist in DB
        /// AND account # does not exist in DB
        /// WHEN requesting account details
        /// THEN it should return null
        /// </summary>
        [Test]
        public void InvalidUserWithInvalidAccountShouldReturnNull()
        {
            var _repo = new AccountRepository(_dbcontext);
            AccountService service = new AccountService(_repo);
            var account = service.GetAccountAsync(INVALID_ACCOUNT, INVALID_USER).Result;
            Assert.IsNull(account);
        }

        /// <summary>
        /// GIVEN a user that exist in DB
        /// AND account # is also exist in DB
        /// WHEN requesting account details
        /// THEN it should return account details
        /// </summary>
        [Test]
        public void ValidUserWithValidAccountShouldReturnOK()
        {
            TestValidUser(VALID_ACCOUNT_WITH_HISTORY, VALID_USER_WITH_HISTORY);
            TestValidUser(VALID_ACCOUNT_WITHOUT_HISTORY, VALID_USER_WITHOUT_HISTORY);

        }

        private void TestValidUser(int accountId, string username)
        {
            var _repo = new AccountRepository(_dbcontext);
            AccountService service = new AccountService(_repo);
            var account = service.GetAccountAsync(accountId, username).Result;

            Assert.IsNotNull(account);
            Assert.AreEqual(accountId, account.AccountNumber);
            Assert.AreEqual(100, account.Balance);
            Assert.IsNotNull(account.PaymentHistory);
            var sortedExpectedPaymentHistory = _expectedPaymentHistory.ToList().OrderByDescending(p => p.Date).ToList();
            var actualPaymmentHistory = account.PaymentHistory.ToList();
            for (var i = 0; i < actualPaymmentHistory.Count(); i++)
            {
                Assert.IsTrue(PaymentAreEqualForTesting(sortedExpectedPaymentHistory[i], actualPaymmentHistory[i]));
            }

        }



        private IEnumerable<PaymentModel> GeneratePayments()
        {
            List<PaymentModel> payments = new List<PaymentModel>();

            DateTime from = DateTime.UtcNow.AddMinutes(-10);

            for (int i = 0; i < 4; i++)
            {
                payments.Add(new PaymentModel() { Amount = i + 10.33M, Status = "Open", Date = from.AddMinutes(i) });
            }
            return payments;


        }

        private bool PaymentAreEqualForTesting(PaymentModel expected, Payment actual)
        {
            return expected.PaymentId == actual.PaymentId &&
                   expected.Amount == actual.Amount &&
                   expected.ClosedReason == actual.ClosedReason &&
                   expected.Date == actual.Date &&
                   expected.Status == actual.Status;
        }
    }
}
