using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;

namespace BotExample
{
    internal static class BotAIClass
    {
        private static Random random = new System.Random(Environment.TickCount);
        private static string _opponentName;
        private static string _lastOpponentsMove;
        private static int _pointstoWin;
        private static int _maxRounds;
        public static int _ourDynamite;
        public static int opponentsDynamiteCount;
        private static List<string> _opponentsMoves;
        private static List<string> _ourMoves;
        public static int _currentRound;

        /* Method called when start instruction is received
         *
         * POST http://<your_bot_url>/start
         *
         */ 
        internal static void SetStartValues(string opponentName, int pointstoWin, int maxRounds, int dynamite)
        {
            _opponentName = opponentName;
            _pointstoWin = pointstoWin;
            _maxRounds = maxRounds;
            _ourDynamite = dynamite;
            opponentsDynamiteCount = dynamite;
            _opponentsMoves = new List<string>();
            _ourMoves = new List<string>();
            _currentRound = 0;
        }

        /* Method called when move instruction is received instructing opponents move
         *
         * POST http://<your_bot_url>/move
         *
         */ 
        public static void DecrementOpponentsDynamiteCount()
        {
            if (_lastOpponentsMove == "DYNAMITE")
            {
                opponentsDynamiteCount--;
            } 
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

        /* Method called when move instruction is received requesting your move
         *
         * GET http://<your_bot_url>/move
         *
         */ 
        
        internal static void StoreOurCurrentMove(string myMove)
        {
            _ourMoves.Add(myMove);
        }

        internal static string GetResultOfLastRound()
        {
            if (_currentRound >= 1 && _ourMoves[_currentRound -1] == _lastOpponentsMove)
            {
                return "DRAW";
            }
            return "Don't know who won, sorry";
        }


        internal static string GetMove()
        {
            string ourMove = CounterSuicideBot();
            StoreOurCurrentMove(ourMove);
            GetResultOfLastRound();
            _currentRound++;
            return ourMove;
        }

        internal static string CounterSuicideBot()
        {
            switch (_lastOpponentsMove)
            
            {
                case "PAPER":
                {
                    return "SCISSORS";
                }
                case "SCISSORS":
                {
                    return "ROCK";

                }
                case "DYNAMITE":
                {
                    return "WATERBOMB";
                }
                default:
                {
                    return "PAPER";
                }

            }
     
        }
    }
}
