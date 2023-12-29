namespace Simbir.GO.Entities;

public class Rent
{
    public int Id { get; set; }
    public int TransportId { get; set; }
    public int UserId { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime? TimeEnd { get; set; }
    public double PriceOfUnit { get; set; }
    public string PriceType { get; set; }
    public double? FinalPrice { get; set; }
}