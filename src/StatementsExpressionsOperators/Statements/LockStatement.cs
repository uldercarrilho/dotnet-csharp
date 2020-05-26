using System;
using System.Threading.Tasks;

namespace Statements
{
    public class LockStatement
    {
        // The lock statement acquires the mutual-exclusion lock for a given object, executes a statement block, and
        // then releases the lock. While a lock is held, the thread that holds the lock can again acquire and release the lock.
        // Any other thread is blocked from acquiring the lock and waits until the lock is released.
        
        // When you synchronize thread access to a shared resource, lock on a dedicated object instance
        // (for example, private readonly object balanceLock = new object();) or
        // another instance that is unlikely to be used as a lock object by unrelated parts of the code.
        // Avoid using the same lock object instance for different shared resources, as it might result in deadlock or lock contention.
        // In particular, avoid using the following as lock objects:
        //     this, as it might be used by the callers as a lock.
        //     Type instances, as those might be obtained by the typeof operator or reflection.
        //     string instances, including string literals, as those might be interned.
        
        // Hold a lock for as short time as possible to reduce lock contention.

        public static void StatementForm(object x)
        {
            lock (x)
            {
                // Your code...
                // You can't use the await operator in the body of a lock statement.
            }
        }

        public static void EquivalentForm(object x)
        {
            object __lockObj = x;
            bool __lockWasTaken = false;
            try
            {
                System.Threading.Monitor.Enter(__lockObj, ref __lockWasTaken);
                // Your code...
            }
            finally
            {
                if (__lockWasTaken) System.Threading.Monitor.Exit(__lockObj);
            }
        }
        
        public static async Task AccountTest()
        {
            var account = new Account(1000);
            var tasks = new Task[100];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => Update(account));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine($"Account's balance is {account.GetBalance()}");
            // Output:
            // Account's balance is 2000
        }

        static void Update(Account account)
        {
            decimal[] amounts = { 0, 2, -3, 6, -2, -1, 8, -5, 11, -6 };
            foreach (var amount in amounts)
            {
                if (amount >= 0)
                {
                    account.Credit(amount);
                }
                else
                {
                    account.Debit(Math.Abs(amount));
                }
            }
        }
    }
    
    public class Account
    {
        // When you synchronize thread access to a shared resource, lock on a dedicated object instance
        private readonly object balanceLock = new object();
        private decimal balance;

        public Account(decimal initialBalance) => balance = initialBalance;

        public decimal Debit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The debit amount cannot be negative.");
            }

            decimal appliedAmount = 0;
            lock (balanceLock)
            {
                if (balance >= amount)
                {
                    balance -= amount;
                    appliedAmount = amount;
                }
            }
            return appliedAmount;
        }

        public void Credit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The credit amount cannot be negative.");
            }

            lock (balanceLock)
            {
                balance += amount;
            }
        }

        public decimal GetBalance()
        {
            lock (balanceLock)
            {
                return balance;
            }
        }
    }
}