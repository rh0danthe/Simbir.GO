using Simbir.GO.DTO;
using Simbir.GO.Entities;
using Simbir.GO.Interface;
using Simbir.GO.Services.Interface;

namespace Simbir.GO.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        this._accountRepository = accountRepository;
    }
    
    public async Task<GetAccountResponse> CreateAccountAsync(AccountDto account)
    {
        var dbAccount = new Account
        {
            Username = account.Username,
            Password = account.Password,
            IsAdmin = account.IsAdmin,
            Balance = account.Balance
        };
        return MapToResponse(await _accountRepository.CreateAsync(dbAccount));
    }

    public async Task<GetAccountResponse?> GetAccountDataAsync(string username, string password)
    {
        return MapToResponse(await _accountRepository.GetAccountData(username, password));
    }

    public async Task<ICollection<GetAccountResponse>> GetAllLimitedAsync(int start, int count)
    {
        var accounts = await _accountRepository.GetAllLimitedAsync(start, count);
        return accounts.Select(ac => MapToResponse(ac)).ToList();
    }

    public async Task<GetAccountResponse> GetByIdAsync(int accountId)
    {
        return MapToResponse(await _accountRepository.GetByIdAsync(accountId));
    }

    public async Task<GetAccountResponse> UpdateAsync(AccountDto account, int accountId)
    {
        var dbAccount = new Account
        {
            Id = accountId,
            Username = account.Username,
            Password = account.Password,
            IsAdmin = account.IsAdmin,
            Balance = account.Balance
        };
        return MapToResponse(await _accountRepository.UpdateAsync(dbAccount));
    }

    public async Task<GetAccountResponse> UpdateInfoAsync(AuthAccountDto account, int accountId)
    {
        var dbAccount = new Account
        {
            Id = accountId,
            Username = account.Username,
            Password = account.Password
        };
        return MapToResponse(await _accountRepository.UpdateAsync(dbAccount));
    }

    public async Task<bool> DeleteAsync(int accountId)
    {
        return await _accountRepository.DeleteAsync(accountId);
    }

    public async Task<GetAccountResponse> AddMoneyAsync(int accountId)
    {
        return MapToResponse(await _accountRepository.AddMoney(accountId));
    }

    private GetAccountResponse MapToResponse(Account account)
    {
        var response = new GetAccountResponse
        {
            Username = account.Username,
            Balance = account.Balance,
            Id = account.Id,
            IsAdmin = account.IsAdmin
        };
        return response;
    }
}