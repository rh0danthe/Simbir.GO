using Simbir.GO.Interface;
using Simbir.GO.Entities;
using Dapper;
using Npgsql;
using Simbir.GO.Common;

namespace Simbir.GO.Repository;

public class AccountRepository : IAccountRepository
{
    private string connectionString;
    
    public AccountRepository(IConfiguration config)
    {
        connectionString = config.GetConnectionString("DefaultConnection");
    }
    
    public async Task<Account> CreateAsync(Account account)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "INSERT INTO \"Accounts\" (\"Username\", \"Password\", \"IsAdmin\", \"Balance\") VALUES(@Username, @Password, @IsAdmin, @Balance) RETURNING *";
            return await db.QueryFirstOrDefaultAsync<Account>(query, account);
        }
    }

    public async Task<Account> GetByIdAsync(int accountId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            string query = "SELECT * FROM \"Accounts\" WHERE \"Id\" = @id";
            var parameters = new { id = accountId };
            return await db.QueryFirstOrDefaultAsync<Account>(query, parameters);
        }
    }

    public async Task<ICollection<Account>> GetAllAsync()
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var accounts = await db.QueryAsync<Account>("SELECT * FROM \"Accounts\"");
            return accounts.ToList();
        }
    }
    
    public async Task<ICollection<Account>> GetAllLimitedAsync(int start, int count)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var accounts = await db.QueryAsync<Account>("SELECT * FROM \"Accounts\" OFFSET @Start FETCH NEXT @Count ROWS ONLY", new {Start = start, Count = count});
            return accounts.ToList();
        }
    }

    public async Task<Account> UpdateAsync(Account account)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query =
                "UPDATE \"Accounts\" SET \"Username\" = @Username, \"Password\" = @Password, \"IsAdmin\" = @IsAdmin, \"Balance\" = @Balance WHERE \"Id\" = @Id RETURNING *";
            return await db.QueryFirstOrDefaultAsync<Account>(query, account);
        }
    }
    
    public async Task<Account> AddMoney(int accountId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            string query = "UPDATE \"Accounts\" SET \"Balance\" = @balance + 250000 WHERE \"Id\" = @id RETURNING *";
            var parameters = new { id = accountId, balance = (await GetByIdAsync(accountId)).Balance};
            return await db.QueryFirstOrDefaultAsync<Account>(query, parameters);
        }
    }

    public async Task<bool> DeleteAsync(int accountId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "DELETE FROM \"Accounts\" WHERE \"Id\" = @id";
            var res = await db.ExecuteAsync(query, new { id = accountId });
            return res > 0;
        }
    }

    public async Task<bool> CheckAccount(string username)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            return await db.QueryFirstOrDefaultAsync<Account>(
                "SELECT \"Id\" FROM \"Accounts\" WHERE \"Username\" = @Username", new { Username = username }) != null;
        }
    }

    public async Task<Account?> GetAccountData(string username, string password)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            return await db.QueryFirstOrDefaultAsync<Account>(
                "SELECT \"Id\", \"IsAdmin\", \"Username\" FROM \"Accounts\" WHERE \"Username\" = @Username and \"Password\"=@Password",
                new {Username = username, Password = password});
        }
    }
}