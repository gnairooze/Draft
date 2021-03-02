using System;
using System.Collections.Generic;
using System.Text;

namespace Dice.Exec
{
    internal class ScreenManager
    {
        internal static int GetDiceMaxNumber()
        {
            int maxNumber = 0;
            bool succeeded = false;

            do
            {
                Console.WriteLine("What is your dice max number? (example 6)");
                string result = Console.ReadLine();

                succeeded = int.TryParse(result, out maxNumber);

                if (succeeded)
                {
                    return maxNumber;
                }
            } while (!succeeded);

            return 0;
        }

        internal static int GetPlayersCount()
        {
            int playersCount = 0;
            bool succeeded = false;

            do
            {
                Console.WriteLine("How many players? (example 2)");
                string result = Console.ReadLine();

                succeeded = int.TryParse(result, out playersCount);

                if (playersCount <= 0)
                {
                    succeeded = false;
                }
                
                if (succeeded)
                {
                    return playersCount;
                }
            } while (!succeeded);

            return 0;
        }

        internal static int GetRollCount()
        {
            int rollCount = 0;
            bool succeeded = false;

            do
            {
                Console.WriteLine("How many rolls per roll? (example 1)");
                string result = Console.ReadLine();

                succeeded = int.TryParse(result, out rollCount);

                if (succeeded)
                {
                    return rollCount;
                }
            } while (!succeeded);

            return 0;
        }

        internal static bool RollAgain()
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

        internal static void Rolled(int trial, string prefix, int[] rolledNumbers)
        {
            Console.WriteLine();
            Console.WriteLine("*********************");
            Console.WriteLine($"* {trial} - {prefix} - Rolled {string.Join(", ", rolledNumbers)} *");
            Console.WriteLine("*********************");
        }

        internal static void ClearScreen()
        {
            Console.Clear();
        }
        internal static void SetColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Blue;
        }
    }
}
