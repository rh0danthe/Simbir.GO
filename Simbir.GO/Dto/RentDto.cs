﻿using System.ComponentModel.DataAnnotations;

namespace Simbir.GO.DTO;

public class RentDto
{
    public int TransportId { get; set; }
    public int UserId { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime? TimeEnd { get; set; }
    public double PriceOfUnit { get; set; }
    [RegularExpression(@"^(Minutes| Days)$", ErrorMessage = "Inappropriate type")]
    public string PriceType { get; set; }
    public double? FinalPrice { get; set; }
}