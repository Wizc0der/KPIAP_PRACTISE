class TransferMoneyCommand : ICommand
{
    private BankAccountService service;
    private decimal amount;
    private string fromAccount;
    private string toAccount;

    public TransferMoneyCommand(BankAccountService service, decimal amount, string fromAccount, string toAccount)
    {
        this.service = service;
        this.amount = amount;
        this.fromAccount = fromAccount;
        this.toAccount = toAccount;
    }

    public void Execute()
    {
        service.Transfer(amount, fromAccount, toAccount);
    }
}