using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;

namespace FBus_BE.Repository
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccount();
        Task<Account> GetAccountById(short id);
        public void Update(Account account);
        Task SaveChanges();
        Task<bool> active(short id);
        Task<bool> deactive(short id);
    }
}