class BankingTerminal
{
    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
    }
}