namespace Simbir.GO.DTO;

public class RentDto
{
    public long TransportId { get; set; }
    public long UserId { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime? TimeEnd { get; set; }
    public double PriceOfUnit { get; set; }
    public string PriceType { get; set; }
    public double? FinalPrice { get; set; }
}