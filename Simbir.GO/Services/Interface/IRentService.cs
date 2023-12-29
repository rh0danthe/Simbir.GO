using Simbir.GO.DTO;
using Simbir.GO.Entities;

namespace Simbir.GO.Services.Interface;

public interface IRentService
{
    public Task<Rent> CreateAsync(RentDto rent);
    public Task<Rent> CreateWithRentTypeAsync(int transportId, string priceType, int userId);
    public Task<Rent> GetByIdAsync(int rentId);
    public Task<ICollection<Rent>> GetAllByUserIdAsync(int userId);
    public Task<ICollection<Rent>> GetAllByTransportIdAsync(int transportId);
    public Task<Rent> FinishRentWithLocationAsync (int rentId, double latitude, double longitude);
    public Task<Rent> UpdateAsync(int rentId, RentDto rent);
    public Task<bool> DeleteAsync(int rentId);
}