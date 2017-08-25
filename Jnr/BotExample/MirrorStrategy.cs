using System;

namespace BotExample
{
    public class MirrorStrategy
    {
        public string GetMove(string lastOpponentsMove)
        {
            Console.WriteLine("Strategy: Mirror");
            return lastOpponentsMove == null || lastOpponentsMove == "WATERBOMB" ? "ROCK" : lastOpponentsMove;
        }
    }
}