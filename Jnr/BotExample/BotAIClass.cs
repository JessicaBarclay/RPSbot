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
        public static int _currentRound;
        public static string[] winList;
        private static readonly Results _Results = 
            new Results { 
                            Win = 0,
                            Lose = 0,
                            Draw = 0
                        };

        internal static void SetStartValues(string opponentName, int pointstoWin, int maxRounds, int dynamite)
        {
            _opponentName = opponentName;
            _pointstoWin = pointstoWin;
            _maxRounds = maxRounds;
            _ourDynamite = dynamite;
            opponentsDynamiteCount = dynamite;
            _opponentsMoves = new List<string>();
            mockedResult = "DRAW";
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
                        return "ROCK";
                    }
                case 1:
                    {
                        Console.WriteLine("---------> Mirror <---------");
                        return "PAPER";
                    }
                default:
                    {
                        Console.WriteLine("---------> Random <---------");
                        return "SCISSORS";
                    }
            }
        }
        
        internal static void StoreOurCurrentMove(string myMove)
        {
            if (_currentRound >= 1) _Results.ourMoves.Add(myMove);
        }

        internal static string GetResultOfLastRound()
        {
            if (_currentRound >= 1)
            {
                if (_Results.ourMoves[_currentRound - 1] == _lastOpponentsMove)
                {
                    _Results.ListOfResults.Add("DRAW");
                    _Results.Draw += 1;
                    return "DRAW";
                }
                else if (DidIWin())
                {
                    _Results.ListOfResults.Add("WIN");
                    _Results.Win += 1;
                    return "WIN";
                }
                else
                {
                    _Results.ListOfResults.Add("LOSE");
                    _Results.Lose += 1;
                    return "LOSE";
                }
            }
            return null;
        }

        public static bool DidIWin()
        {
            string lastResult = _Results.ourMoves[_currentRound - 1] + _lastOpponentsMove;
            int position = 0;
            for (position = 0; position < winList.Length; position++)
            {
                if (winList[position] == lastResult) return true;
            }
            return false;
        }

        internal static string GetMove()
        {
            Console.WriteLine("Round " + _currentRound);
            Console.WriteLine("Win:  " + _Results.Win);
            Console.WriteLine("Lose: " + _Results.Lose);
            Console.WriteLine("Draw: " + _Results.Draw);
            var ourMove = SwitchStrategies();
            StoreOurCurrentMove(ourMove);
            GetResultOfLastRound();
            _currentRound++;
            return ourMove;
        }
    }
}
