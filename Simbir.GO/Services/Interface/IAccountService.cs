using Simbir.GO.Entities;

namespace Simbir.GO.Services.Interface;

public interface IAccountService
{
    Task<Account> CreateAccount(Account account);
    Task<Account?> GetAccountData(string username, string password);
    public Task<Account> GetByIdAsync(int accountId);
}