namespace BotExample
{
    public class DetectMirrorBot
    {
        public static bool GetMove(string opponentsMoves)
        {
            try
            {
                string expectedMirr = "ROCKROCKROCK";
                string firstThree = "";
                if (opponentsMoves.Length >= 3)
                {
                    for (var i = 0; i < 3; i++)
                    {
                        firstThree += opponentsMoves[i];
                    }
                }
                return expectedMirr == firstThree ? true : false;
            }
            catch
            {
                return false;
            }
        }
    }
}