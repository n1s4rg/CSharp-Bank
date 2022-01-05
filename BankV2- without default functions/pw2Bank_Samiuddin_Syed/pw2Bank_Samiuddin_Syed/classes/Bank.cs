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

        //used to custom sort functionality names in alphabetical order, no inbuilt functions used
        // for example
        // unsorted name = Sami, Saif
        // sorted order is = Saif, Sami
        // as first, second character are S,a but third character i comes before m so, Saif comes before Sami
        //return list of same names indexes
        public static List<int> sortTheNames(List<List<int>> notSortedBalanceIndex, int i,bool sortfamilyName)
        {
            int swapped = 0;
            List<int> sameNameIndexList = new List<int>();
            
            //looping till the list is sorted, using bubble sort
            for (;;)
            {
                //to track if list is sorted or not
                swapped = 0;
                for (int j = 0; j < notSortedBalanceIndex[i].Count - 1; j++)
                {
                    string oneName, twoName;
                    
                    //checking which names to sort, true means we are sorting family names
                    if (sortfamilyName)
                    {
                         oneName = Accounts[notSortedBalanceIndex[i][j]].Client.FamilyName.ToLower();
                         twoName = Accounts[notSortedBalanceIndex[i][j + 1]].Client.FamilyName.ToLower();
                    } else
                    {
                        oneName = Accounts[notSortedBalanceIndex[i][j]].Client.GivenName.ToLower();
                        twoName = Accounts[notSortedBalanceIndex[i][j + 1]].Client.GivenName.ToLower();
                    }

                    int ffnamelength = oneName.Length;
                    int sfnamelength = twoName.Length;

                    //which is smallest in length, and we loop till there
                    int nth = ffnamelength > sfnamelength ? sfnamelength : ffnamelength;

                    //storing ascii values of string in form of array
                    byte[] ffnameBytes = Encoding.ASCII.GetBytes(oneName);
                    byte[] snameBytes = Encoding.ASCII.GetBytes(twoName);
                    
                    //to track if there is same name
                    bool sameName = false;

                    for (int k = 0; k < nth; k++)
                    {
                        //checking ascii values of each character
                        if (ffnameBytes[k] > snameBytes[k])
                        {
                            //second goes first, SWAP
                            int temp = notSortedBalanceIndex[i][j];
                            notSortedBalanceIndex[i][j] = notSortedBalanceIndex[i][j + 1];
                            notSortedBalanceIndex[i][j + 1] = temp;

                            //to track if list is sorted
                            swapped++;
                            sameName = false;
                            break;
                        }
                        //it means the earlier name is already sorted and we exited from this iteration
                        else if (ffnameBytes[k] < snameBytes[k])
                        {
                            sameName = false;
                            break;
                        }
                        else
                            sameName = true;
                    }

                    //if the names are same, then we are adding them to list to further sort them
                    if (sameName)
                    {
                        sameNameIndexList.Add(notSortedBalanceIndex[i][j]);
                        sameNameIndexList.Add(notSortedBalanceIndex[i][j+1]);
                    }
                }

                //if there was no swapping done, then it means we have sorted list
                if (swapped <= 0)
                    break;
            }

            //removing duplicate indexes from the list
            if (sameNameIndexList.Count > 2)
            {
                for(int u = 0;u< sameNameIndexList.Count - 1;u++)
                {
                    if(sameNameIndexList[u] == sameNameIndexList[u + 1])
                        sameNameIndexList.RemoveAt(u);
                }
            }

            //returning the indexes of same names to further sort them
            return sameNameIndexList;
        }


        //custom sorting function to sort accounts in ascending order
        //Balance -> Family Name -> Given Name in this order,
        //if same balance then Family name,
        //if family name is same then given name,
        public static void sortAccountsInAsc()
        {
            //our custom sort functionality
            int n = Accounts.Count;
            bool sameBalanceFlag = false;
            
            //using bubbleSort to sort the balances
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (Accounts[j].Balance > Accounts[j + 1].Balance)
                    {
                        Account temp = Accounts[j];
                        Accounts[j] = Accounts[j + 1];
                        Accounts[j + 1] = temp;
                    } 
                    else if(Accounts[j].Balance == Accounts[j + 1].Balance)
                        sameBalanceFlag = true;
                }
            }

            //if there is same balance for accounts
            if (sameBalanceFlag)
            { 
                //to store indexes of same balances accounts
                List<List<int>> sameBalanceIndex = new List<List<int>>();
                List<int> subList = new List<int>();
                
                double prev = 0.0;

                //generating indexes of same balances in list, adding no duplicate indexes
                for (int p = 0; p < n - 1; p++)
                {
                    if (p == 0)
                        prev = Accounts[p].Balance;

                    if(prev != Accounts[p].Balance && subList.Count > 1)
                    {
                        sameBalanceIndex.Add(subList.ToList());
                        subList.Clear();
                        prev = Accounts[p].Balance;
                    } 
                    else if(prev != Accounts[p].Balance)
                    {
                        prev = Accounts[p].Balance;
                    }

                    if (Accounts[p].Balance == Accounts[p + 1].Balance)
                    {
                        if (subList.Count > 0)
                        {
                            if (subList[subList.Count - 1] != p)
                                subList.Add(p);
                        }
                        else
                        {
                            subList.Add(p);
                        }

                        subList.Add(p + 1);
                    }
                }

                //list of indexes for same balances 
                sameBalanceIndex.Add(subList.ToList());
                List<List<int>> oldSamBalanceIndex = new List<List<int>>();

                //creating a new copy samebalanceindex list 
                foreach(List<int> p in sameBalanceIndex)
                    oldSamBalanceIndex.Add(p.ToList());

                subList.Clear();

                List<List<int>> sameFamilyNameList = new List<List<int>>();

                //sorting family names  and getting same family name list
                for (int i = 0;i< sameBalanceIndex.Count; i++)
                {
                    List<int> oneFamilyList = sortTheNames(sameBalanceIndex, i,true);
                    
                    if (oneFamilyList.Count > 0)
                        sameFamilyNameList.Add(oneFamilyList);    

                }
                    
                //swapping the accounts list for family names
                if (sameBalanceIndex.Count > 0)
                {
                    for (int i = 0; i < sameBalanceIndex.Count; i++)
                    {
                        for(int j = 0;j < sameBalanceIndex[i].Count-1; j++)
                        {
                            Account tempAccount = Accounts[oldSamBalanceIndex[i][j]];
                            Accounts[oldSamBalanceIndex[i][j]] = Accounts[sameBalanceIndex[i][j]];
                            Accounts[sameBalanceIndex[i][j]] = tempAccount;
                        }

                    }
                }

                //temp list used for swapping the names in accounts list
                List<List<int>> oldSameFamilyNameList = new List<List<int>>();

                //creating a new copy sameFamilyNameList list 
                foreach (List<int> p in sameFamilyNameList)
                    oldSameFamilyNameList.Add(p.ToList());

                //sorting given names, as there are same family names
                if (sameFamilyNameList.Count > 0)
                {
                    for (int i = 0; i < sameFamilyNameList.Count; i++)
                        sortTheNames(sameFamilyNameList, i, false);
                    
                    //swapping
                    for (int i = 0; i < sameFamilyNameList.Count; i++)
                    {
                        for (int j = 0; j < sameFamilyNameList[i].Count - 1; j++)
                        {
                            Account tempAccount = Accounts[oldSameFamilyNameList[i][j]];
                            Accounts[oldSameFamilyNameList[i][j]] = Accounts[sameFamilyNameList[i][j]];
                            Accounts[sameFamilyNameList[i][j]] = tempAccount;
                        }
                    }
                }
            }
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
