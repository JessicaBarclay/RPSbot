using System;

namespace BotExample
{
    public class FinalSalvo
    {
        private static readonly RandomStrategy _RandomStrategy = new RandomStrategy();
       

        public string Fire( string previousMove)
        {

            if (previousMove == "DYNAMITE")
                {
                    return _RandomStrategy.GetMove();
                }
                return "DYNAMITE";
  
        }

    }
}