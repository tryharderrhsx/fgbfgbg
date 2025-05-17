using System.ComponentModel.DataAnnotations;

namespace InternetTenderService.Models;

public class Bid
{
    public int Id { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public DateTime PlacedAt { get; set; } = DateTime.UtcNow;

    public int TenderId { get; set; }
    public Tender Tender { get; set; }

    public int BidderId { get; set; }
    public User Bidder { get; set; }
}
