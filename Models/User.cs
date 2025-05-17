namespace InternetTenderService.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }

    public List<Tender> Tenders { get; set; }
    public List<Bid> Bids { get; set; }
}
