using System;
using System.Collections.Generic;

namespace BotExample
{
    public static class BotAIClass
    {
        private static string _lastOpponentsMove;
        public static int _ourDynamite;
        public static int opponentsDynamiteCount;
        private static List<string> _opponentsMoves;
        public static int _currentRound;
        public static string[] winList;
        public static string ourPreviousMove;
        private static readonly MirrorStrategy _MirrorStrategy = new MirrorStrategy();
        private static readonly DirectCounterStrategy _DirectCounterStrategy = new DirectCounterStrategy();

        private static Results _Results;

        internal static void SetStartValues(string opponentName, int pointstoWin, int maxRounds, int dynamite)
        {
            _ourDynamite = dynamite;
            opponentsDynamiteCount = dynamite;
            _opponentsMoves = new List<string>();
            _lastOpponentsMove = "";
            _currentRound = 0;
            ourPreviousMove = "";
            winList = new [] {
                                    "DYNAMITEROCK","DYNAMITEPAPER","DYNAMITESCISSORS",
                                    "ROCKWATERBOMB","ROCKSCISSORS", "PAPERWATERBOMB","PAPERROCK",
                                    "SCISSORSWATERBOMB","SCISSORSPAPER", "WATERBOMBDYNAMITE"
                                   };
            _Results = new Results
            {
                Win = 0,
                Lose = 0,
                Draw = 0
            };
        }
      
        public static void DecrementOpponentsDynamiteCount()
        {
            if (_lastOpponentsMove == "DYNAMITE") opponentsDynamiteCount--;
        }

        public static void StoreOpponentsMoves()
        {
            _opponentsMoves.Add(_lastOpponentsMove);
        }

        public static void SetLastOpponentsMove(string lastOpponentsMove)
        {
            _lastOpponentsMove = lastOpponentsMove;
            DecrementOpponentsDynamiteCount();
            StoreOpponentsMoves();
        }

        internal static string DetermineMove()
        {
            if (_ourDynamite != 0 && DidIDraw())
            {
                _ourDynamite--;
                return "DYNAMITE";
            }
            return SwitchStrategies();
        }

        internal static bool DidIDraw()
        {
            return _lastOpponentsMove == ourPreviousMove;
        }

        internal static bool CalculateWinLossDifference()
        {
            return _Results.Lose - _Results.Win >= 3;
        }

        internal static string SwitchStrategies()
        {
            if (CalculateWinLossDifference())
            {
                return _MirrorStrategy.GetMove(_lastOpponentsMove);
            }
            return _DirectCounterStrategy.GetMove(_lastOpponentsMove);
        }

        internal static void StoreOurCurrentMove(string myMove)
        {
            _Results.ourMoves.Add(myMove);
        }

        internal static string GetResultOfLastRound()
        {
            if (DidIDraw())
            {
                _Results.ListOfResults.Add("DRAW");
                _Results.Draw += 1;
                return "DRAW";
            }

            if (DidIWin())
            {
                _Results.ListOfResults.Add("WIN");
                _Results.Win += 1;
                return "WIN";
            }

            _Results.ListOfResults.Add("LOSE");
            _Results.Lose += 1;
            return "LOSE";
        }

        public static bool DidIWin()
        {
            string lastResult = ourPreviousMove + _lastOpponentsMove;
            for (var position = 0; position < winList.Length; position++)
            {
                if (winList[position] == lastResult) return true;
            }
            return false;
        }

        internal static string GetMove()
        {
            try
            {
                _currentRound++;
                var ourMove = DetermineMove();
                StoreOurCurrentMove(ourMove);
                GetResultOfLastRound();
                ourPreviousMove = ourMove;
                Console.WriteLine("Round " + _currentRound);
                Console.WriteLine("Win:  " + _Results.Win);
                Console.WriteLine("Lose: " + _Results.Lose);
                Console.WriteLine("Draw: " + _Results.Draw);
                Console.WriteLine("-----------------------------------");
                return ourMove;
            }
            catch
            {
                return "ROCK";
            }
        }
    }
}
