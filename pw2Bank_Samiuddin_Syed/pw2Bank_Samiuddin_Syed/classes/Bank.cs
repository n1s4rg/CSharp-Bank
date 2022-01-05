using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pw2Bank_Samiuddin_Syed.classes
{
    class Bank
    {
        private static List<Account> accounts = new List<Account>();

        internal static List<Account> Accounts { get => accounts; }

        public static bool AddAccount(Account acc, Client client)
        {
            Random rand = new Random();
            long randAccNum;

            do
            {
                //10000 - 10099 
                randAccNum = rand.Next(10000, 10099);

            } while(GetIndexOfAccount(randAccNum) >= 0); //if account number available then generate another random num
            
            //only 100 accounts can be added
            if (Accounts.Count < 100)
            {
                Account account = new Account(randAccNum, acc.Balance, client);
                Accounts.Add(account);
                return true;
            }

            return false;
            
        }

        

        public static bool RemoveAccount(long accNum)
        {
            //gets index of the account in list or else return -1
            int indx = GetIndexOfAccount(accNum);
            if (indx >= 0)
            {
                Accounts.RemoveAt(indx);
                return true;
            }
                
            return false;
        }

        public static int GetIndexOfAccount(long accNum)
        {
            //gets index of the account in list or else return -1
            foreach(Account acc in Accounts)
            {
                if (acc.AccountNumber.Equals(accNum))
                {
                    return Accounts.IndexOf(acc);
                }
            }
            return -1;
        }
        public static bool Deposit(long accountNumber, double depositBalance)
        {
            //get index of account in list
            int indx = GetIndexOfAccount(accountNumber);

            if (indx >= 0)
            {
                Accounts[indx].Balance = Accounts[indx].Balance + depositBalance;
                return true;
            }

            return false;
        }

        /**
         * returns -1 for account not found
         * returns -2 for insufficient balance
         * returns >= 0 for success
         */
        public static int Withdraw(long accountNumber, double withdrawBalance)
        {
            int indx = GetIndexOfAccount(accountNumber);

            //checking if account is available
            if (indx >= 0)
            {
                double availableBalance =  Accounts[indx].Balance;

                if (availableBalance >= withdrawBalance)
                {
                    Accounts[indx].Balance = Accounts[indx].Balance - withdrawBalance;
                    return 1;
                }
                else
                {
                    return -2;
                }
            }

            return -1;
        }
        public static void sortAccountsInAsc()
        {
            //sorting by balance then familyname, then given name,in ascending order
            Accounts.OrderBy(ac => ac.Balance).ThenBy(ac => ac.Client.FamilyName)
                            .ThenBy(ac => ac.Client.GivenName).ToList();
        }

        public static String DisplayInfo(long accountNumber)
        {  
           
            foreach (Account acc in Accounts)
            {
                if (acc.AccountNumber.Equals(accountNumber))
                {
                    return "\nAccount Number: "+acc.AccountNumber + "\nGiven Name: " +acc.Client.GivenName
                        +"\nFamily Name: "+ acc.Client.FamilyName + "\nBalance: " + acc.Balance;
                    
                }
            }
            return "\nNo account found!";  
        }

        public static double GetAverageBalanceAccounts()
        {
            int accountsCount = Accounts.Count;

            return GetTotalBalanceAccounts() / accountsCount;
        }

        public static double GetTotalBalanceAccounts()
        {
            double sum = 0;
   
            foreach (Account accs in Accounts)
                sum = sum + accs.Balance;

            return sum;
        }

    }
}
