using Simbir.GO.DTO;
using Simbir.GO.Entities;

namespace Simbir.GO.Services.Interface;

public interface ITransportService
{
    public Task<Transport> CreateUserAsync(int userId, UserCreateTransportDto transport);
    public Task<Transport> CreateAdminAsync(AdminCreateTransportDto transport);
    public Task<Transport> GetByIdAsync(int transportId);
    public Task<ICollection<Transport>> GetAllAsync();
    public Task<ICollection<Transport>> GetByParamsAsync(double centerLatitude, double centerLongitude,
        double radius, string type);
    public Task<ICollection<Transport>> GetAllLimitedAsync(int start, int count, string transportType);
    public Task<Transport> UpdateAsync(int transportId, UpdateTransportDto transport);
    public Task<bool> DeleteAsync(int transportId);
}