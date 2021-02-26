using System;

namespace Dice.Exec
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit ...");
            Console.Read();
        }

        static int GetDiceMaxNumber()
        {
            int maxNumber = 0;
            bool succeeded = false;

            do
            {
                Console.WriteLine("What is your dice max number? (example 8)");
                string result = Console.ReadLine();

                succeeded = int.TryParse(result, out maxNumber);

                if (succeeded)
                {
                    return maxNumber;
                }
            } while (!succeeded);

            return 0;
        }

        private static bool RollAgain()
        {
            Console.WriteLine("Roll Again? ([Y]es, [N]o)");
            var result = Console.ReadKey();

            if (result.Key == ConsoleKey.N)
            {
                return false;
            }

            if (result.Key == ConsoleKey.Enter)
            {
                Console.WriteLine("Y");
            }

            return true;
        }

        private static void Start()
        {
            var business = new Dice.Business.Manager();

            business.MaxNumber = GetDiceMaxNumber();

            int rolledNumber = business.Roll();
            Console.WriteLine($"Rolled {rolledNumber}");

            while (RollAgain())
            {
                rolledNumber = business.Roll();
                Console.WriteLine($"Rolled {rolledNumber}");

            }
        }

        
    }
}
