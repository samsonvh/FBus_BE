using AutoMapper;
using FBus_BE.DTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.DTOs.PageResponses;
using FBus_BE.Models;
using Microsoft.EntityFrameworkCore;
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
            orderDict = new Dictionary<string, Expression<Func<Account, object?>>>()
            {
                {"id", account => account.Id},
                {"code", account => account.Code}
            };
        }

        public async Task<DefaultPageResponse<AccountDTO>> GetAccountList(AccountPageRequest pageRequest)
        {
            DefaultPageResponse<AccountDTO> pageResponse = new DefaultPageResponse<AccountDTO>();
            if (pageRequest.PageIndex == null)
            {
                pageRequest.PageIndex = 1;
            }
            if (pageRequest.PageSize == null)
            {
                pageRequest.PageSize = 10;
            }
            if (pageRequest.OrderBy == null)
            {
                pageRequest.OrderBy = "id";
            }
            int skippedCount = (int)((pageRequest.PageIndex - 1) * pageRequest.PageSize);
            int totalCount = await _context.Accounts
                                         .Where(account => pageRequest.Code != null ? account.Code.Contains(pageRequest.Code) : true)
                                         .Where(account => pageRequest.Email != null ? account.Email.Contains(pageRequest.Email) : true)
                                         .CountAsync();
            if (totalCount > 0)
            {
                List<AccountDTO> accounts = pageRequest.Direction == "desc"
                    ? await _context.Accounts.Skip(skippedCount)
                                             .OrderByDescending(orderDict[pageRequest.OrderBy.ToLower()])
                                             .Where(account => pageRequest.Code != null ? account.Code.Contains(pageRequest.Code) : true)
                                             .Where(account => pageRequest.Email != null ? account.Email.Contains(pageRequest.Email) : true)
                                             .Select(account => _mapper.Map<AccountDTO>(account))
                                             .ToListAsync()
                    : await _context.Accounts.Skip(skippedCount)
                                             .OrderBy(orderDict[pageRequest.OrderBy.ToLower()])
                                             .Where(account => pageRequest.Code != null ? account.Code.Contains(pageRequest.Code) : true)
                                             .Where(account => pageRequest.Email != null ? account.Email.Contains(pageRequest.Email) : true)
                                             .Select(account => _mapper.Map<AccountDTO>(account))
                                             .ToListAsync();
                pageResponse.Data = accounts;
            }
            pageResponse.PageIndex = (int)pageRequest.PageIndex;
            pageResponse.PageCount = (int)(totalCount / pageRequest.PageSize) + 1;
            pageResponse.PageSize = (int)pageRequest.PageSize;
            return pageResponse;
        }

        public async Task<AccountDTO> GetAccountDetail(int id)
        {
            Account? account = await _context.Accounts.FirstOrDefaultAsync(account => account.Id == id);
            return _mapper.Map<AccountDTO>(account);
        }
    }
}
