using FBus_BE.DTOs;
using FBus_BE.Models;
using FBus_BE.Services.SortingModel;

namespace FBus_BE.Services
{
    public interface IAccountService
    {
        Task<PageResponse<AccountDTO>> GetAccountsWithPaging(PageRequest pageRequest);
        Task<AccountDTO> GetAccountDetails(int id);
    }
}
