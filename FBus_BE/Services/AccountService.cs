using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Account>> GetAllAccount()
        {
            return await _accountRepository.GetAllAccount();
        }
    }
}