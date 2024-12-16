public class Transaction
{
    public Guid Transaction_id { get; init; }
    public Guid User_id { get; init; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public int? Reference_id { get; set; } = null;
}
