﻿using Simbir.GO.Entities;

namespace Simbir.GO.Interface;

public interface ITransportRepository
{
    public Task<Transport> CreateAsync(Transport transport);
    public Task<Transport> GetByIdAsync(int transportId);
    public Task<ICollection<Transport>> GetAllAsync();
    
    public Task<ICollection<Transport>> GetAllLimitedAsync(int start, int count, string transportType);
    public Task<Transport> UpdateAsync(Transport transport);
    public Task<bool> DeleteAsync(int transportId);
}