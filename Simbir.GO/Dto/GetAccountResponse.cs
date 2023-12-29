namespace Simbir.GO.DTO;

public class GetAccountResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public bool IsAdmin { get; set; }
    public double Balance { get; set; }
}