using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BotInterface.Bot;
using BotInterface.Game;

namespace ExampleBot
{
    public class ExampleBot : IBot
    {
        Random rnd = new Random();
        public string LastMove;
        public int ResponseCounter;
        public int DynamiteCount;
        public Dictionary<Move, int> Repeater;
        public Dictionary<Move, int> CountLastMove;
        public Dictionary<Move, Move> Response;
        public List<Move> Moves;
        public ExampleBot()
        {
            CountLastMove = new Dictionary<Move, int>
            {
                { Move.D, 0 },
                { Move.W, 0 },
                { Move.R, 0 },
                { Move.S, 0 },
                { Move.P, 0 }
            };
            Repeater = new Dictionary<Move, int>
            {
                { Move.D, 0 },
                { Move.W, 0 },
                { Move.R, 0 },
                { Move.S, 0 },
                { Move.P, 0 }
            };
            Response = new Dictionary<Move, Move>
            {
                { Move.P, Move.S},
                { Move.R, Move.P},
                { Move.S, Move.R},
                { Move.D, Move.W},
                { Move.W, Move.R}


            };
            Moves = new List<Move>();
        }

        public Move MakeMove(Gamestate gamestate)
        {
            if (gamestate.GetRounds().Length == 0)
            {
                return RandomMove();
            }
            var lastMove = gamestate.GetRounds().Last();
            var p1Move = lastMove.GetP1();
            var p2Move = lastMove.GetP2();
            Moves.Add(p2Move);
            CountLastMove[p2Move]++;

            if (Repeater[p2Move] > 2 && DynamiteCount < 100)
            {
                ResponseCounter++;
                if (ResponseCounter == 2 && DynamiteCount <= 100)
                {
                    ResponseCounter = 0;
                    return RandomMove();
                }
                return Response[p2Move];
            }

            if (ResponseCounter > 50)
            {
                return RandomMoveWithoutDynamiteOrWater();
            }

            if (Response[p1Move] == Move.D)
            {
                DynamiteCount++;

            }

            foreach (var move in Moves)
            {
                if (CountLastMove[move] == 2)
                {
                    Repeater[move]++;
                    CountLastMove[move] = 0;
                    return Response[move];
                }
            }

            if (DynamiteCount >= 100)
            {
                return RandomMoveWithoutDynamiteOrWater();
            }

            return RandomMove();
        }

        public Move RandomMove()
        {
            var rng = rnd.Next(1, 5);

            switch (rng)
            {
                case 1:
                    return Move.P;
                case 2:
                    return Move.R;
                case 3:
                    return Move.S;
                case 4:
                    DynamiteCount++;
                    return Move.D;
                case 5:
                    return Move.W;
                default:
                    return Move.P;
            }
        }

        public Move RandomMoveWithoutDynamiteOrWater()
        {
            var rng = rnd.Next(1, 3);

            switch (rng)
            {
                case 1:
                    return Move.P;
                case 2:
                    return Move.R;
                case 3:
                    return Move.S;
                default:
                    return Move.P;
            }
        }
    }
}
