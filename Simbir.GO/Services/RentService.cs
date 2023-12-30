using Simbir.GO.DTO;
using Simbir.GO.Entities;
using Simbir.GO.Interface;
using Simbir.GO.Services.Interface;

namespace Simbir.GO.Services;

public class RentService : IRentService
{
    private readonly IRentRepository _rentRepository;
    private readonly ITransportRepository _transportRepository;
    private readonly IAccountRepository _accountRepository;
    public RentService(IRentRepository rentRepository, ITransportRepository transportRepository, IAccountRepository accountRepository)
    {
        _rentRepository = rentRepository;
        _transportRepository = transportRepository;
        _accountRepository = accountRepository;
    }
    public async Task<Rent> CreateAsync(RentDto rent)
    {
        var dbRent = new Rent
        {
            TransportId = rent.TransportId,
            UserId = rent.UserId,
            TimeStart = rent.TimeStart,
            TimeEnd = rent.TimeEnd,
            PriceOfUnit = rent.PriceOfUnit,
            PriceType = rent.PriceType,
            FinalPrice = rent.FinalPrice
        };
        return await _rentRepository.CreateAsync(dbRent);
    }

    public async Task<Rent> CreateWithRentTypeAsync(int transportId, string priceType, int userId)
    {
        var transport = await _transportRepository.GetByIdAsync(transportId);
        if (!transport.CanBeRented)
            throw new Exception();
        var dbRent = new Rent
        {
            TransportId = transportId,
            UserId = userId,
            TimeStart = DateTime.Now,
            PriceType = priceType
        };
        if (priceType == "Minutes")
        {
            dbRent.PriceOfUnit = (double)transport.MinutePrice!; 
        }
        else if (priceType == "Days")
        {
            dbRent.PriceOfUnit = (double)transport.DayPrice!;
        }
        return await _rentRepository.CreateAsync(dbRent);
    }

    public async Task<Rent> GetByIdAsync(int rentId)
    {
        return await _rentRepository.GetByIdAsync(rentId);
    }

    public async Task<ICollection<Rent>> GetAllByUserIdAsync(int userId)
    {
        var rent = await _rentRepository.GetAllAsync();
        return rent.Where(r => r.UserId == userId).ToList();
    }

    public async Task<ICollection<Rent>> GetAllByTransportIdAsync(int transportId)
    {
        var rent = await _rentRepository.GetAllAsync();
        return rent.Where(r => r.TransportId == transportId).ToList();
    }

    public async Task<Rent> FinishRentWithLocationAsync(int rentId, double latitude, double longitude)
    {
        var rent = await _rentRepository.GetByIdAsync(rentId);
        var transport = await _transportRepository.GetByIdAsync(rent.TransportId);
        transport.Latitude = latitude;
        transport.Longitude = longitude;
        await _transportRepository.UpdateAsync(transport);
        rent.TimeEnd = DateTime.Now;
        var difference = rent.TimeEnd - rent.TimeStart;
        if (rent.PriceType == "Minutes")
        {
            rent.FinalPrice = rent.PriceOfUnit * difference.GetValueOrDefault().Minutes;
        }
        else if (rent.PriceType == "Days")
        {
            rent.FinalPrice = rent.PriceOfUnit * difference.GetValueOrDefault().Days;
        }

        var user = await _accountRepository.GetByIdAsync(rent.UserId);
        user.Balance -= (double)rent.FinalPrice!;
        await _accountRepository.UpdateAsync(user);
        return await _rentRepository.UpdateAsync(rent);
    }

    public async Task<Rent> UpdateAsync(int rentId, RentDto rent)
    {
        var dbRent = new Rent
        {
            Id = rentId,
            TransportId = rent.TransportId,
            UserId = rent.UserId,
            TimeStart = rent.TimeStart,
            TimeEnd = rent.TimeEnd,
            PriceOfUnit = rent.PriceOfUnit,
            PriceType = rent.PriceType,
            FinalPrice = rent.FinalPrice
        };
        return await _rentRepository.UpdateAsync(dbRent);
    }

    public async Task<bool> DeleteAsync(int rentId)
    {
        return await _rentRepository.DeleteAsync(rentId);
    }
}