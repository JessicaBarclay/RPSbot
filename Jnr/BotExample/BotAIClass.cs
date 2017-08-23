using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace BotExample
{
    internal static class BotAIClass
    {
        private static readonly Random random = new System.Random(Environment.TickCount);
        private static string _opponentName;
        private static string _lastOpponentsMove;
        private static int _pointstoWin;
        private static int _maxRounds;
        public static int _ourDynamite;
        public static int opponentsDynamiteCount;
        private static List<string> _opponentsMoves;
        public static int _currentRound;
        public static string[] winList;
        public static string ourPreviousMove;
        private static readonly MirrorStrategy _MirrorStrategy = new MirrorStrategy();
        private static readonly RandomStrategy _RandomStrategy = new RandomStrategy();
        private static readonly DirectCounterStrategy _DirectCounterStrategy = new DirectCounterStrategy();
        private static readonly DetectMirrorBot _DetectMirrorBot = new DetectMirrorBot();

        private static Results _Results;

        internal static void SetStartValues(string opponentName, int pointstoWin, int maxRounds, int dynamite)
        {
            _opponentName = opponentName;
            _pointstoWin = pointstoWin;
            _maxRounds = maxRounds;
            _ourDynamite = dynamite;
            opponentsDynamiteCount = dynamite;
            _opponentsMoves = new List<string>();
            _lastOpponentsMove = "";
            _currentRound = 0;
            ourPreviousMove = "";
            winList = new string[] {
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

        internal static string responseIfDraw()
        {
            if (_ourDynamite != 0 && _lastOpponentsMove == ourPreviousMove)
            {
                _ourDynamite--;
                return "DYNAMITE";
            }
            return SwitchStrategies();
        }

        internal static bool CalculateWinLossDifference()
        {
            return _Results.Lose - _Results.Win >= 3;
        }

        internal static string SwitchStrategies()
        {
            if (CalculateWinLossDifference())
            {
                Console.WriteLine("Strategy: Direct");
                return _DirectCounterStrategy.GetMove(_lastOpponentsMove);
            }

            Console.WriteLine("Strategy: Mirror");
            return _MirrorStrategy.GetMove(_lastOpponentsMove);
        }

        internal static void StoreOurCurrentMove(string myMove)
        {
            if (_currentRound >= 1) _Results.ourMoves.Add(myMove);
        }

        internal static string GetResultOfLastRound()
        {
            if (_currentRound >= 1)
            {
                if (ourPreviousMove == _lastOpponentsMove)
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
            return null;
        }

        public static bool DidIWin()
        {
            string lastResult = ourPreviousMove + _lastOpponentsMove;
            int position;
            for (position = 0; position < winList.Length; position++)
            {
                if (winList[position] == lastResult) return true;
            }
            return false;
        }

        internal static string GetMove()
        {
            _currentRound++;
            var ourMove = responseIfDraw();
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
    }
}
