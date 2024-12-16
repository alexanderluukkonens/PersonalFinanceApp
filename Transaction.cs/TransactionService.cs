public interface ITransactionService
{
    Transaction AddTransaction(decimal amount, string description);
    Transaction DeleteTransaction();
    Transaction ShowCurrentBalance();
    Transaction ShowTransactions();
}
