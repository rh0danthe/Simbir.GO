using Simbir.GO.Entities;

namespace Simbir.GO.Interface;

public interface IRentRepository
{
    public Task<Rent> CreateAsync(Rent rent);
    public Task<Rent> GetByIdAsync(int rentId);
    public Task<Rent> GetByTransportAsync(int transportId);
    public Task<Rent> GetByUserAsync(int userId);
    public Task<ICollection<Rent>> GetAllAsync();
    public Task<Rent> UpdateAsync(Rent rent);
    public Task<Rent> FinishRentAsync(int rentId);
    public Task<bool> DeleteAsync(int rentId);
}
