namespace Simbir.GO.DTO;

public class GetTransportByDistanceDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Radius { get; set; }
    public string TransportType { get; set; }
}