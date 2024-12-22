public interface ITransactionService
{
    void AddTransaction(decimal amount, string description);
    void DeleteTransaction(Guid transactionId);
    decimal? ShowCurrentBalance();
    Transaction ShowTransactions();
    List<Transaction> GetTransactionsByTimespan(DateTime startDate, DateTime endDate);
    List<Transaction> GetTransactionsByCustomDates(DateTime startDate, DateTime endDate);
    bool VerifyTransactionOwnership(Guid transactionId);
}
