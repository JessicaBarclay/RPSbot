﻿using System;
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
        internal static string GetMove()
        {
            int rnd = random.Next(3);
            switch (rnd)
            {
                case 0:
                    {
                        Console.WriteLine("This is the Direct Counter Strategy");
                        return DirectCounterStrategy();
                    }
                case 1:
                    {
                        Console.WriteLine("This is the Mirror Strategy");
                        return MirrorStrategy();
                    }
                default:
                    {
                        Console.WriteLine("This is the Random Strategy");
                        return RandomStrategy();
                    }
            }
        }

        internal static string DirectCounterStrategy()
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

        internal static string MirrorStrategy()
        {
            return _lastOpponentsMove == null || _lastOpponentsMove == "WATERBOMB" ? "ROCK" : _lastOpponentsMove;
        }


        internal static bool IsMirrorBot()
        {
            try
            {
                string expectedMirr = "ROCKROCKROCK";
                string firstThree = "";
                if (_opponentsMoves.Count >= 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        firstThree += _opponentsMoves[i];
                    }
                }
                if (expectedMirr == firstThree)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        internal static string RandomStrategy()
        {
            int rnd = random.Next(3);
            switch (rnd)
            {
                case 0:
                    {
                        return "ROCK";
                    }
                case 1:
                    {
                        return "SCISSORS";
                    }
                default:
                    {
                        return "PAPER";
                    }

            }
        }
    }
}
