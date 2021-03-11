using Microsoft.EntityFrameworkCore;
using MoulaCodingChallenge.Contracts;
using MoulaCodingChallenge.Data;
using MoulaCodingChallenge.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoulaCodingChallenge.Services
{

    /// <summary>
    /// Service layer - Account service interface
    /// </summary>
    public interface IAccountService
    {
        Task<Account> GetAccountAsync(int accountId, string userName);
    }
    /// <summary>
    /// Service layer - Account Sercice
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Account> GetAccountAsync(int accountId, string userName)
        {
            Account result = null;

            var account = await _accountRepository.RetrieveAccount(accountId, userName);

            if (account != null)
            {
                result = new Account()
                {
                    AccountNumber = account.AccountNumber,
                    Balance = account.Balance,
                    PaymentHistory = account.PaymentHistory.Select(p => new Payment()
                    {
                        PaymentId = p.PaymentId,
                        Date = p.Date,
                        Amount = p.Amount,
                        Status = p.Status,
                        ClosedReason = p.ClosedReason
                    })
                };
            }
            return result;

        }
    }

}
