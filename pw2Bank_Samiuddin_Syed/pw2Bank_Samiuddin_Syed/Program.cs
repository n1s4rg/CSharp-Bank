
using pw2Bank_Samiuddin_Syed.classes;
using System;
using System.Linq;

/*
 * Team members
 * 
    Abhiroop Singh Abhiroop Singh
    Danish Danish
    Nisarg Kumar Ashokbhai Prajapati
    Deep ParkashKumar Vagehla
    Syed, Samiuddin
    Waghmare, Abhijeet Shekhar
 
 */
namespace pw2Bank_Samiuddin_Syed
{
    class Program
    {
        static void Main(string[] args)
        {

            int choice = -1;

            do
            {
                Console.WriteLine("\n\tMenu\n=======================\n1. Add a bank account" +
                    "\n2. Remove a bank account(by Account Number)" +
                    "\n3. Display Client's information(by Account Number)" +
                    "\n4. Deposit to particular account(by Account Number)" +
                    "\n5. Withdrawal from particular account(by Account Number)" +
                    "\n6. Sort and Display clients information" +
                    "\n7. Display average balance of accounts" +
                    "\n8. Display total balance of accounts" +
                    "\n9. Exit");
                try
                {
                    Console.Write("\nEnter your choice: ");
                    choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            //Add Account
                            addAccount();
                            break;
                        case 2:
                            //Remove account by account num
                            removeAccount();
                            break;
                        case 3:
                            //Display single account information by account number
                            Console.Write("\nEnter Client's account number: ");
                            long accNum = Convert.ToInt64(Console.ReadLine());

                            Console.WriteLine("\nClients Information\n========================");
                            Console.WriteLine("\n" + Bank.DisplayInfo(accNum));
                            break;
                        case 4:
                            //Deposit
                            deposit();
                            break;
                        case 5:
                            //Withdraw
                            withdraw();
                            break;
                        case 6:
                            //Sort and Display
                            Bank.sortAccountsInAsc();
                            displayAllAccountsInfo();
                            break;
                        case 7:
                            //average balance of accounts
                            Console.WriteLine("Average balance of accounts: "
                                + Math.Round(Bank.GetAverageBalanceAccounts(), 6));
                            break;
                        case 8:
                            //Total balance of accounts
                            Console.WriteLine("Total balance of accounts: "
                                    + Math.Round(Bank.GetTotalBalanceAccounts(), 6));
                            break;
                        case 9:
                            Console.WriteLine("Goodbye!");
                            break;
                        default:
                            Console.WriteLine(">> Invalid Choice! Try again! <<");
                            break;
                    }
                } catch(Exception e)
                {
                    Console.WriteLine("\n"+e.Message);
                }
                

            } while (choice != 9);

            Console.ReadLine();
        }

        public static void addAccount()
        {
            //Add Account
            Console.Write("\nEnter Client's Given Name: ");
            String givenName = Console.ReadLine();
            Console.Write("Enter Client's Family Name: ");
            String familyName = Console.ReadLine();

            Client client = new Client(givenName, familyName);

            Console.Write("Enter Client's Balance: ");
            double balance = Convert.ToDouble(Console.ReadLine());

            Account acc = new Account(balance, client);

            if (Bank.AddAccount(acc, client))
                Console.WriteLine("\nAccount added successfully!");
            else
                Console.WriteLine("\nAccounts limit reached(Only 100 accounts)");
        }

        public static void removeAccount()
        {
            Console.Write("\nEnter Client's account number: ");
            long accNumber = Convert.ToInt64(Console.ReadLine());

            if (Bank.RemoveAccount(accNumber))
                Console.WriteLine("\nClient's account is deleted!");
            else
                Console.WriteLine("\nNo account found!");
        }

        public static void deposit()
        {
            Console.Write("\nEnter Client's account number: ");
            long acctNum = Convert.ToInt64(Console.ReadLine());
            Console.Write("\nEnter amount to deposit: ");
            double amount = Convert.ToDouble(Console.ReadLine());

            if (Bank.Deposit(acctNum, amount))
                Console.WriteLine("\nAmount Deposited successfully");
            else
                Console.WriteLine("\nNo account found!");
        }

        public static void withdraw()
        {
            Console.Write("\nEnter Client's account number: ");
            long atNum = Convert.ToInt64(Console.ReadLine());

            Console.Write("\nEnter amount to withdraw: ");
            double withAmt = Convert.ToDouble(Console.ReadLine());

            /**
            * returns -1 for account not found
            * returns -2 for insufficient balance
            * returns >= 0 for success
            */
            int status = Bank.Withdraw(atNum, withAmt);

            if (status > 0)
                Console.WriteLine("Withdraw successfully!");
            else if (status == -1)
                Console.WriteLine("\nNo account found!");
            else if (status == -2)
                Console.WriteLine("\nInsufficient balance!");
        }



        public static void displayAllAccountsInfo()
        {
            if (Bank.Accounts.Count() > 0)
            {
                Console.WriteLine("\nClients Information");
                foreach (Account aacc in Bank.Accounts)
                {
                    Console.WriteLine("=====================================");
                    Console.WriteLine(Bank.DisplayInfo(aacc.AccountNumber));
                }
            }
            else
                Console.WriteLine("\nList is empty!");
        }
    }
}
