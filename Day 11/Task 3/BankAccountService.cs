interface ICommand
{
    void Execute();
}

class BankAccountService
{
    public void Transfer(decimal amount, string fromAccount, string toAccount)
    {
        Console.WriteLine($"Transferred ${amount} from {fromAccount} to {toAccount}");
    }
}
