using Simbir.GO.Interface;
using Simbir.GO.Entities;
using Dapper;
using Npgsql;

namespace Simbir.GO.Repository;

public class RentRepository : IRentRepository
{
    private string connectionString;
    
    public RentRepository(IConfiguration config)
    {
        connectionString = config.GetConnectionString("DefaultConnection");
    }
    
    public async Task<Rent> CreateAsync(Rent rent)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "INSERT INTO \"Rent\" (\"TransportId\", \"UserId\", \"TimeStart\", \"TimeEnd\", \"PriceOfUnit\", \"PriceType\", \"FinalPrice\") VALUES(@TransportId, @UserId, @TimeStart, @TimeEnd, @PriceOfUnit, @PriceType, @FinalPrice) RETURNING *";
            return await db.QueryFirstOrDefaultAsync<Rent>(query, rent);
        }
    }

    public async Task<Rent> GetByIdAsync(int rentId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            string query = "SELECT * FROM \"Rent\" WHERE \"Id\" = @id";
            var parameters = new { id = rentId };
            return await db.QueryFirstOrDefaultAsync<Rent>(query, parameters);
        }
    }

    public async Task<Rent> GetByTransportAsync(int transportId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            string query = "SELECT * FROM \"Rent\" WHERE \"TransportId\" = @TransportId";
            var parameters = new { TransportId = transportId };
            return await db.QueryFirstOrDefaultAsync<Rent>(query, parameters);
        }
    }

    public async Task<Rent> GetByUserAsync(int userId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            string query = "SELECT * FROM \"Rent\" WHERE \"UserId\" = @UserId";
            var parameters = new { UserId = userId };
            return await db.QueryFirstOrDefaultAsync<Rent>(query, parameters);
        }
    }

    public async Task<ICollection<Rent>> GetAllAsync()
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var rent = await db.QueryAsync<Rent>("SELECT * FROM \"Rent\"");
            return rent.ToList();
        }
    }

    public async Task<Rent> UpdateAsync(Rent rent)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query =
                "UPDATE \"Rent\" SET \"TransportId\" = @TransportId, \"UserId\" = @UserId, \"TimeStart\" = @TimeStart, \"TimeEnd\" = @TimeEnd, \"PriceOfUnit\" = @PriceOfUnit, \"PriceType\" = @PriceType, \"FinalPrice\" = @FinalPrice WHERE \"UserId\" = @UserId RETURNING *";
            return await db.QueryFirstOrDefaultAsync<Rent>(query, rent);
        }
    }

    public async Task<Rent> FinishRentAsync(int rentId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            Rent rent = await db.QueryFirstOrDefaultAsync<Rent>("SELECT * FROM \"Rent\" WHERE \"Id\" = @id", new {id = rentId});
            Account account = await db.QueryFirstOrDefaultAsync<Account>("SELECT * FROM \"Rent\" WHERE \"UserId\" = @userId", new {userId = rent.UserId});
            string query = "WITH UPDATE \"Rent\" SET \"TimeEnd\" = @TimeEnd WHERE \"RentId\" = @RentId RETURNING * JOIN UPDATE \"Accounts\" SET \"Balance\" = @balance - @finalPrice WHERE \"UserId\" = @userId";
            var parameters = new { RentId = rentId, TimeEnd = DateTime.UtcNow, balance = account.Balance, finalPrice = rent.FinalPrice, userId = account.Id };
            return await db.QueryFirstOrDefaultAsync<Rent>(query, parameters);
        }
    }

    public async Task<bool> DeleteAsync(int rentId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "DELETE FROM \"Rent\" WHERE \"RentId\" = @RentId";
            var res = await db.ExecuteAsync(query, new { RentId = rentId });
            return res > 0;
        }
    }
}