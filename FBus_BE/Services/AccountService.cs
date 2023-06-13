using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Dto;
using FBus_BE.Models;
using FBus_BE.Repository;

namespace FBus_BE.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Account> GetAccountById(short id)
        {
            return await _accountRepository.GetAccountById(id)!;
        }

        public async Task<List<Account>> GetAccounts(int page)
        {
            return await _accountRepository.GetAccounts(page);
        }

        public async Task<IEnumerable<AccountResponse>> GetAccounts(int page, int pageSize)
        {
            var accounts = await _accountRepository.GetAllAccount();
            var accountResponse = accounts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AccountResponse
                {
                    Id = a.Id,
                    Email = a.Email,
                    Role = a.Role,
                    Status = a.Status
                }).ToList();
            return accountResponse;
        }

        public async Task<IEnumerable<Account>> GetAllAccount()
        {
            return await _accountRepository.GetAllAccount();
        }
    }
}