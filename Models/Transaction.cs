public class Transaction
{
    public Guid Transaction_id { get; init; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
}
