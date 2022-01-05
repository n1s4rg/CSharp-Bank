using System;

namespace pw2Bank_Samiuddin_Syed.classes
{
    class Account
    {
        long accountNumber;
        double balance;
        private Client client;

        public Account(long accountNumber, double balance, Client client)
        {
            this.accountNumber = accountNumber;
            Balance = balance;
            this.Client = client;
        }
        public Account(double balance, Client client)
        {
            Balance = balance;
            this.Client = client;
        }

        public long AccountNumber
        {
            get => accountNumber;
        }

        public double Balance { get => balance; set => balance = value; }
        internal Client Client { get => client; set => client = value; }


    }
}
