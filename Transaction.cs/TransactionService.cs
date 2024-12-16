public interface ITransactionService
{
    void AddTransaction(decimal amount, string description);
    void DeleteTransaction(Guid transactionId);
    decimal? ShowCurrentBalance();
    Transaction ShowTransactions();
}
