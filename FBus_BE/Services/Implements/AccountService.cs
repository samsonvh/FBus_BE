using AutoMapper;
using FBus_BE.DTOs;
using FBus_BE.Models;
using FBus_BE.Services.SortingModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace FBus_BE.Services.Implements
{
    public class AccountService : IAccountService
    {
        private readonly Dictionary<string, Expression<Func<Account, object?>>> orderDict;
        private readonly FbusMainContext _context;
        private readonly IMapper _mapper;

        public AccountService(FbusMainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.orderDict = new Dictionary<string, Expression<Func<Account, object?>>>()
            {
                {"id", account => account.Id},
                {"code", account => account.Code}
            };
        }

        public async Task<PageResponse<AccountDTO>> GetAccountsWithPaging(PageRequest pageRequest)
        {
            if (pageRequest != null)
            {
                if (pageRequest.OrderBy == null)
                {
                    pageRequest.OrderBy = "id";
                }
                int skippedCount = (pageRequest.PageNumber - 1) * pageRequest.PageSize;
                List<AccountDTO> accounts = pageRequest.Direction == "desc"
                    ? await _context.Accounts.Skip(skippedCount)
                                       .Take(pageRequest.PageSize)
                                       .OrderByDescending(orderDict[pageRequest.OrderBy])
                                       .Select(account => _mapper.Map<AccountDTO>(account))
                                       .ToListAsync()
                    : await _context.Accounts.Skip(skippedCount)
                                       .Take(pageRequest.PageSize)
                                       .OrderBy(orderDict[pageRequest.OrderBy])
                                       .Select(account => _mapper.Map<AccountDTO>(account))
                                       .ToListAsync();
                return new PageResponse<AccountDTO>
                {
                    Items = accounts,
                    PageIndex = pageRequest.PageNumber,
                    PageCount = (_context.Accounts.Count() / pageRequest.PageSize) + 1,
                    PageSize = pageRequest.PageSize
                };
            }
            return null;
        }

        public async Task<AccountDTO> GetAccountDetails(int id)
        {
            Account? account = await _context.Accounts
                .FirstOrDefaultAsync(account => account.Id == id);
            return _mapper.Map<AccountDTO>(account);
        }

    }
}
