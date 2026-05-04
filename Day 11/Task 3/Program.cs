using System;


class Program
{
    static void Main()
    {
        var service = new BankAccountService();
        var terminal = new BankingTerminal();

        ICommand command1 = new TransferMoneyCommand(service, 1000, "ACC001", "ACC002");
        ICommand command2 = new TransferMoneyCommand(service, 500, "ACC003", "ACC004");

        terminal.ExecuteCommand(command1);
        terminal.ExecuteCommand(command2);
    }
}