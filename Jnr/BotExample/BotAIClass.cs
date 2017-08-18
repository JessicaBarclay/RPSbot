using System;
using System.Collections.Generic;

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
        public static string mockedResult;
        private static List<string> _ourMoves;
        private static List<string> _results;
        public static int _currentRound;
        public static string[] winList;
        private static readonly MirrorStrategy _MirrorStrategy = new MirrorStrategy();
        private static readonly RandomStrategy _RandomStrategy = new RandomStrategy();
        private static readonly DirectCounterStrategy _DirectCounterStrategy = new DirectCounterStrategy();
        private static readonly DetectMirrorBot _DetectMirrorBot = new DetectMirrorBot();

        internal static void SetStartValues(string opponentName, int pointstoWin, int maxRounds, int dynamite)
        {
            _opponentName = opponentName;
            _pointstoWin = pointstoWin;
            _maxRounds = maxRounds;
            _ourDynamite = dynamite;
            opponentsDynamiteCount = dynamite;
            _opponentsMoves = new List<string>();
            mockedResult = "DRAW";
            _ourMoves = new List<string>();
            _results = new List<string>();
            _currentRound = 0;
            winList = new string[] {
                                    "DYNAMITEROCK","DYNAMITEPAPER","DYNAMITESCISSORS",
                                    "ROCKWATERBOMB","ROCKSCISSORS", "PAPERWATERBOMB","PAPERROCK",
                                    "SCISSORSWATERBOMB","SCISSORSPAPER", "WATERBOMBDYNAMITE"
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
            if (_ourDynamite != 0 && mockedResult == "DRAW")
            {
                _ourDynamite--;
                return "DYNAMITE";
            }
            return SwitchStrategies();
        }

        internal static string SwitchStrategies()
        {
            int rnd = random.Next(3);
            switch (rnd)
            {
                case 0:
                    {
                        Console.WriteLine("---------> Direct <---------");
                        return _DirectCounterStrategy.GetMove(_lastOpponentsMove);
                    }
                case 1:
                    {
                        Console.WriteLine("---------> Mirror <---------");
                        return _MirrorStrategy.GetMove(_lastOpponentsMove);
                    }
                default:
                    {
                        Console.WriteLine("---------> Random <---------");
                        return _RandomStrategy.GetMove();
                    }
            }
        }
        
        internal static void StoreOurCurrentMove(string myMove)
        {
            if (_currentRound >= 1) _ourMoves.Add(myMove);
        }

        internal static string GetResultOfLastRound()
        {
            if (_currentRound >= 1)
            {
                if (_ourMoves[_currentRound - 1] == _lastOpponentsMove)
                {
                    _results.Add("DRAW");
                    return "DRAW";
                }
                else if (DidIWin())
                { 
                    _results.Add("WIN");
                    return "WIN";
                }
                else
                {
                    _results.Add("LOSE");
                    return "LOSE";
                }
            }
            return null;
        }

        public static bool DidIWin()
        {
            string lastResult = _ourMoves[_currentRound - 1] + _lastOpponentsMove;
            int position = 0;
            for (position = 0; position < winList.Length; position++)
            {
                if (winList[position] == lastResult) return true;
            }
            return false;
        }

        internal static string GetMove()
        {
            var ourMove = SwitchStrategies();
            StoreOurCurrentMove(ourMove);
            // GetResultOfLastRound is actually returning the result of the current Round...
            GetResultOfLastRound();
            _currentRound++;
            return ourMove;
        }
    }
}
