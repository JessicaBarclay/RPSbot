using System.Collections.Generic;

namespace BotExample
{
    public class Results
    {
        public int Win { get; set; }
        public int Lose { get; set; }
        public int Draw { get; set; }
        public List<string> ourMoves = new List<string>();
        public List<string> ListOfResults = new List<string>();
    }
}