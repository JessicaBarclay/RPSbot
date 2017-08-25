namespace BotExample
{
    public class MirrorStrategy
    {
        public string GetMove(string lastOpponentsMove)
        {
            if (lastOpponentsMove == "DYNAMITE")
            {
                return "ROCK";
            }
            return lastOpponentsMove == null || lastOpponentsMove == "WATERBOMB" ? "ROCK" : lastOpponentsMove;
        }
    }
}