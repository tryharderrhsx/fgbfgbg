using System.ComponentModel.DataAnnotations;

namespace InternetTenderService.Models;

public class Tender
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int OwnerId { get; set; }
    public User Owner { get; set; }

    public string? ImagePath { get; set; }


    public List<Bid> Bids { get; set; }
}
