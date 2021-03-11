using Microsoft.EntityFrameworkCore;
using MoulaCodingChallenge.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoulaCodingChallenge.Data
{
    /// <summary>
    /// Data access layer - Account Repo interface
    /// </summary>
    public interface IAccountRepository
    {
        Task<AccountModel> RetrieveAccount(int accountId, string userName);
    }
    /// <summary>
    /// Data access layer - Account Repo 
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _dbContext;

        public AccountRepository(AccountContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<AccountModel> RetrieveAccount(int accountId, string userName)
        {
            return await _dbContext.Accounts.Select(a => new AccountModel()
            {
                AccountNumber = a.AccountNumber,
                Balance = a.Balance,
                UserName = a.UserName,
                PaymentHistory = a.PaymentHistory.OrderByDescending(p => p.Date).ToList()
            }).FirstOrDefaultAsync(a => a.AccountNumber == accountId && a.UserName.ToUpper() == userName.ToUpper()); // assuming Username is case insensitive
        }
    }
}
