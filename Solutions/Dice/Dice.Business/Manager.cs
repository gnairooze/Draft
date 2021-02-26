using System;

namespace Dice.Business
{
    public class Manager
    {
        public int MaxNumber { get; set; }
        public int RollCount { get; set; }
        public int[] Roll()
        {
            var result = new Random();
            int[] rolls = new int[RollCount];

            for (int i = 0; i < RollCount; i++)
            {
                rolls[i] = result.Next(1, MaxNumber + 1);
            }

            return rolls;
        }
    }
}
