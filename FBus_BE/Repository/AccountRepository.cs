using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;
using FBus_BE.Data;
using Microsoft.EntityFrameworkCore;

namespace FBus_BE.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FbusMainContext _context;
        public AccountRepository(FbusMainContext context)
        {
            _context = context;
        }

        public async Task<bool> active(short id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return false;

            account.Status = "ACTIVE";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deactive(short id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return false;
            account.Status = "INACTIVE";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Account> GetAccountById(short id)
        {
            return await _context.Accounts.FindAsync(id)!;
        }

        public async Task<IEnumerable<Account>> GetAllAccount()
        {
            return await _context.Accounts.ToListAsync();
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(Account account)
        {
            _context.Accounts.Update(account);
        }
    }
}