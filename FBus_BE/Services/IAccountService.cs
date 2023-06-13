using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Dto;
using FBus_BE.Models;

namespace FBus_BE.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccount();
        Task<Account> GetAccountById(short id);
        Task<IEnumerable<AccountResponse>> GetAccounts(int page, int pageSize);
    }
}