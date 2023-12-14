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
    
    public async Task<Account> CreateAccount(Account account)
    {
        return await _accountRepository.CreateAsync(account);
    }

    public async Task<Account?> GetAccountData(string username, string password)
    {
        return await _accountRepository.GetAccountData(username, password);
    }

    public Task<Account> GetByIdAsync(int accountId)
    {
        throw new NotImplementedException();
    }
}