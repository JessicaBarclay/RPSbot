namespace BotExample
{
    public class MirrorStrategy
    {
        public string GetMove(string lastOpponentsMove)
        {
            return lastOpponentsMove == null || lastOpponentsMove == "WATERBOMB" ? "ROCK" : lastOpponentsMove;
        }
    }
}