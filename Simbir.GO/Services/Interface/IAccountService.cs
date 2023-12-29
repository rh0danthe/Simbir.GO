using Simbir.GO.DTO;
using Simbir.GO.Entities;

namespace Simbir.GO.Services.Interface;

public interface IAccountService
{
    public Task<GetAccountResponse> CreateAccountAsync(AccountDto account);
    public Task<GetAccountResponse?> GetAccountDataAsync(string username, string password);
    public Task<ICollection<GetAccountResponse>> GetAllLimitedAsync(int start, int count);
    public Task<GetAccountResponse> GetByIdAsync(int accountId);
    public Task<GetAccountResponse> UpdateAsync(AccountDto account, int accountId);
    public Task<GetAccountResponse> UpdateInfoAsync(AuthAccountDto account, int accountId);
    public Task<bool> DeleteAsync(int accountId);
    public Task<GetAccountResponse> AddMoneyAsync(int accountId);
}