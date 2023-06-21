using FBus_BE.DTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.DTOs.PageResponses;

namespace FBus_BE.Services
{
    public interface IAccountService
    {
        Task<DefaultPageResponse<AccountDTO>> GetAccountList(AccountPageRequest pageRequest);
        Task<AccountDTO> GetAccountDetail(int id);
    }
}
