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

        private static void Start()
        {
            ScreenManager.ClearScreen();
            ScreenManager.SetColors();

            var business = new Dice.Business.Manager();

            business.MaxNumber = ScreenManager.GetDiceMaxNumber();
            business.RollCount = ScreenManager.GetRollCount();

            int[] rolledNumbers = business.Roll();
            ScreenManager.Rolled(1, rolledNumbers);

            int trial = 2;

            while (ScreenManager.RollAgain())
            {
                ScreenManager.ClearScreen();

                rolledNumbers = business.Roll();
                ScreenManager.Rolled(trial++, rolledNumbers);
            }
        }

        
    }
}
