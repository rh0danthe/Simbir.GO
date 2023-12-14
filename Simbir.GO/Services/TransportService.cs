using Simbir.GO.Entities;
using Simbir.GO.Interface;
using Simbir.GO.Repository;
using Simbir.GO.Services.Interface;

namespace Simbir.GO.Services;

public class TransportService : ITransportService
{
    private readonly ITransportRepository _repository;
    public TransportService(ITransportRepository repository)
    {
        _repository = repository;
    }
    public async Task<Transport> CreateAsync(Transport transport)
    {
        return await _repository.CreateAsync(transport);
    }

    public Task<Transport> GetByIdAsync(int transportId)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Transport>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Transport>> GetAllLimitedAsync(int start, int count, string transportType)
    {
        throw new NotImplementedException();
    }

    public Task<Transport> UpdateAsync(Transport transport)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int transportId)
    {
        throw new NotImplementedException();
    }
}