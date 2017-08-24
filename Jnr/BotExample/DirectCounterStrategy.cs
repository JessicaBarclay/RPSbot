using System;

namespace BotExample
{
    public class DirectCounterStrategy
    {
        public string GetMove(string lastOpponentsMove)
        {
            Console.WriteLine("Strategy: Direct");
            switch (lastOpponentsMove)
            {
                case "PAPER":
                {
                    return "SCISSORS";
                }
                case "SCISSORS":
                {
                    return "ROCK";

                }
                case "DYNAMITE":
                {
                    return "WATERBOMB";
                }
                default:
                {
                    return "PAPER";
                }
            }
        }
    }
}