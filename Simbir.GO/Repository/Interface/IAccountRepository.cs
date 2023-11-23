using Simbir.GO.Entities;

namespace Simbir.GO.Interface;

public interface IAccountRepository
{
    public Task<Account> CreateAsync(Account account);
    public Task<Account> GetByIdAsync(int accountId);
    public Task<ICollection<Account>> GetAllAsync();
    public Task<ICollection<Account>> GetAllLimitedAsync(int start, int count);
    public Task<Account> UpdateAsync(Account account);
    public Task<Account> AddMoney(int accountId);
    public Task<bool> DeleteAsync(int accountId);
}