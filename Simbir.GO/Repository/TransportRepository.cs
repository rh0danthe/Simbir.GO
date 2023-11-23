using Simbir.GO.Interface;
using Simbir.GO.Entities;
using Dapper;
using Npgsql;

namespace Simbir.GO.Repository;

public class TransportRepository : ITransportRepository
{
    private string connectionString;
    
    public TransportRepository(IConfiguration config)
    {
        connectionString = config.GetConnectionString("DefaultConnection");
    }
    
    public async Task<Transport> CreateAsync(Transport transport)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "INSERT INTO \"Transport\" (\"OwnerId\", \"CanBeRented\", \"TransportType\", \"Model\", \"Color\", \"Identifier\", \"Description\", \"Latitude\", \"Longitude\", \"MinutePrice\", \"DayPrice\") VALUES(@OwnerId, @CanBeRented, @TransportType, @Model, @Color, @Identifier, @Description, @Latitude, @Longitude, @MinutePrice, @DayPrice) RETURNING *";
            return await db.QueryFirstOrDefaultAsync<Transport>(query, transport);
        }
    }

    public async Task<Transport> GetByIdAsync(int transportId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            string query = "SELECT * FROM \"Transport\" WHERE \"Id\" = @id";
            var parameters = new { id = transportId };
            return await db.QueryFirstOrDefaultAsync<Transport>(query, parameters);
        }
    }

    public async Task<ICollection<Transport>> GetAllAsync()
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var accounts = await db.QueryAsync<Transport>("SELECT * FROM \"Transport\"");
            return accounts.ToList();
        }
    }
    
    public async Task<ICollection<Transport>> GetAllLimitedAsync(int start, int count, string transportType)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var transport = await db.QueryAsync<Transport>("SELECT * FROM \"Transport\" WHERE \"TransportType\" = @TransportType OFFSET @Start FETCH NEXT @Count ROWS ONLY", new {Start = start, Count = count, TransportType = transportType});
            return transport.ToList();
        }
    }

    public async Task<Transport> UpdateAsync(Transport transport)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query =
                "UPDATE \"Transport\" SET \"OwnerId\" = @OwnerId, \"CanBeRented\" = @CanBeRented, \"TransportType\" = @TransportType, \"Model\" = @Model, \"Color\" = @Color, \"Identifier\" = @Identifier, \"Description\" = @Description, \"Latitude\" = @Latitude, \"Longitude\" = @Longitude, \"MinutePrice\" = @MinutePrice, \"DayPrice\" = @DayPrice WHERE \"Id\" = @Id RETURNING *";
            return await db.QueryFirstOrDefaultAsync<Transport>(query, transport);
        }
    }

    public async Task<bool> DeleteAsync(int transportId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "DELETE FROM \"Transport\" WHERE \"Id\" = @id";
            var res = await db.ExecuteAsync(query, new { id = transportId });
            return res > 0;
        }
    }
}