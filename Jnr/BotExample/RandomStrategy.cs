using System;

namespace BotExample
{
    public class RandomStrategy
    {
        private static readonly Random random = new System.Random(Environment.TickCount);
        public string GetMove()
        {
            var rnd = random.Next(3);
            switch (rnd)
            {
                case 0:
                {
                    return "ROCK";
                }
                case 1:
                {
                    return "SCISSORS";
                }
                default:
                {
                    return "PAPER";
                }
            }
        }
    }
}