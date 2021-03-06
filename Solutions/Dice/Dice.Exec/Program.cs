﻿using System;

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

            int playersCount = ScreenManager.GetPlayersCount();

            int[] rolledNumbers = business.Roll();

            int trial = 1;

            int playerNumber = trial % playersCount;
            if (playerNumber == 0)
            {
                playerNumber = playersCount;
            }

            ScreenManager.Rolled(trial, $"player {playerNumber}", rolledNumbers);

            trial++;

            while (ScreenManager.RollAgain())
            {
                ScreenManager.ClearScreen();

                rolledNumbers = business.Roll();

                playerNumber = trial % playersCount;
                if(playerNumber == 0)
                {
                    playerNumber = playersCount;
                }

                ScreenManager.Rolled(trial, $"player {playerNumber}", rolledNumbers);

                trial++;
            }
        }

        
    }
}
