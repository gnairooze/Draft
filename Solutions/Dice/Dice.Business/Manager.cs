using System;

namespace Dice.Business
{
    public class Manager
    {
        public int MaxNumber { get; set; }
        public int Roll()
        {
            var result = new Random();

            return result.Next(1, MaxNumber+1);
        }
    }
}
