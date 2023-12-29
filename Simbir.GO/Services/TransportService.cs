using Simbir.GO.DTO;
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
    public async Task<Transport> CreateAdminAsync(AdminCreateTransportDto transport)
    {
        var dbTransport = new Transport();
        dbTransport.OwnerId = transport.OwnerId;
        dbTransport.CanBeRented = transport.CanBeRented;
        dbTransport.TransportType = transport.TransportType;
        dbTransport.Model = transport.Model;
        dbTransport.Color = transport.Color;
        dbTransport.Identifier = transport.Identifier;
        dbTransport.Description = transport.Description;
        dbTransport.Latitude = transport.Latitude;
        dbTransport.Longitude = transport.Longitude;
        dbTransport.MinutePrice = transport.MinutePrice;
        dbTransport.DayPrice = transport.DayPrice;
        return await _repository.CreateAsync(dbTransport);
    }
    
    public async Task<Transport> CreateUserAsync(int userId, UserCreateTransportDto transport)
    {
        var dbTransport = new Transport();
        dbTransport.OwnerId = userId;
        dbTransport.CanBeRented = transport.CanBeRented;
        dbTransport.TransportType = transport.TransportType;
        dbTransport.Model = transport.Model;
        dbTransport.Color = transport.Color;
        dbTransport.Identifier = transport.Identifier;
        dbTransport.Description = transport.Description;
        dbTransport.Latitude = transport.Latitude;
        dbTransport.Longitude = transport.Longitude;
        dbTransport.MinutePrice = transport.MinutePrice;
        dbTransport.DayPrice = transport.DayPrice;
        return await _repository.CreateAsync(dbTransport);
    }

    public async Task<Transport> GetByIdAsync(int transportId)
    {
        return await _repository.GetByIdAsync(transportId);
    }

    public async Task<ICollection<Transport>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    
    public async Task<ICollection<Transport>> GetByParamsAsync(double centerLatitude, double centerLongitude, double radius, string type)
    {
        var allTransport = await _repository.GetAllAsync();
        var availableTransport = new List<Transport>();
        foreach (var transport in allTransport)
        {
            if (transport.TransportType != type)
                continue;
            double centerLatitudeRad = ToRadians(centerLatitude);
            double centerLongitudeRad = ToRadians(centerLongitude);
            double transportLatitude = ToRadians(transport.Latitude);
            double transportLongitude = ToRadians(transport.Longitude);
            double distance = DistanceBetweenPoints(centerLatitudeRad, centerLongitudeRad, transportLatitude,
                transportLongitude);
            if (distance <= radius)
                availableTransport.Add(transport);
        }

        return availableTransport;
    }
    
    private double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
    
    private double DistanceBetweenPoints(double lat1, double lon1, double lat2, double lon2)
    {
        const double EarthRadiusKm = 6371;

        double dLat = lat2 - lat1;
        double dLon = lon2 - lon1;

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(lat1) * Math.Cos(lat2) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return EarthRadiusKm * c;
    }

    public async Task<ICollection<Transport>> GetAllLimitedAsync(int start, int count, string transportType)
    {
        return await _repository.GetAllLimitedAsync(start, count, transportType);
    }

    public async Task<Transport> UpdateAsync(int transportId, UpdateTransportDto transport)
    {
        var dbTransport = new Transport();
        dbTransport.Id = transportId;
        dbTransport.OwnerId = transport.OwnerId;
        dbTransport.CanBeRented = transport.CanBeRented;
        dbTransport.TransportType = transport.TransportType;
        dbTransport.Model = transport.Model;
        dbTransport.Color = transport.Color;
        dbTransport.Identifier = transport.Identifier;
        dbTransport.Description = transport.Description;
        dbTransport.Latitude = transport.Latitude;
        dbTransport.Longitude = transport.Longitude;
        dbTransport.MinutePrice = transport.MinutePrice;
        dbTransport.DayPrice = transport.DayPrice;
        return await _repository.UpdateAsync(dbTransport);
    }

    public async Task<bool> DeleteAsync(int transportId)
    {
        return await _repository.DeleteAsync(transportId);
    }
}