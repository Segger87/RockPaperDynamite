using System;
using System.Collections.Generic;
using BotInterface.Bot;
using BotInterface.Game;

namespace ExampleBot
{
    public class ExampleBot : IBot
    {
        Random rnd = new Random();
        public string LastMove;
        public int PaperRepeater;
        public int ScissorRepeater;
        public int RockRepeater;
        public int DynamiteRepeater;
        public int WaterRepeater;
        public int DynamiteCount;
        public Dictionary<Move, int> Repeater;
        public Dictionary<Move, int> countLastMove;

        public ExampleBot()
        {
            countLastMove.Add(Move.D, 0);
            countLastMove.Add(Move.W, 0);
            countLastMove.Add(Move.R, 0);
            countLastMove.Add(Move.S, 0);
            countLastMove.Add(Move.P, 0);
            Repeater.Add(Move.D, 0);
            Repeater.Add(Move.W, 0);
            Repeater.Add(Move.R, 0);
            Repeater.Add(Move.S, 0);
            Repeater.Add(Move.P, 0);
        }

        public Move MakeMove(Gamestate gamestate)
        {
            if (countLastMove[Move.P] == 3)
            {
                PaperRepeater += 1;
                countLastMove[Move.P] = 0;
                return Scissors();
            }

            if (countLastMove[Move.R] == 3)
            {
                RockRepeater += 1;
                countLastMove[Move.R] = 0;
                return Paper();
            }

            if (countLastMove[Move.S] == 3)
            {
                ScissorRepeater += 1;
                countLastMove[Move.S] = 0;
                return Rock();
            }

            if (countLastMove[Move.D] == 3)
            {
                DynamiteRepeater += 1;
                countLastMove[Move.D] = 0;
                return Water();
            }

            if (countLastMove[Move.W] == 3)
            {
                Repeater[Move.W] += 1;
                countLastMove[Move.W] = 0;
                return Rock();
            }

            if (DynamiteCount >= 100)
            {
                return RandomMoveWithoutDynamiteOrWater();
            }

            return RandomMove();
        }



        public Move PlayMove(Move move)
        {
            Repeater[move] += 1;
            countLastMove[move] = 0;
            return Rock();
        }

        public Move Paper()
        {
            return Move.P;
        }

        public Move Scissors()
        {
            return Move.S;
        }

        public Move Rock()
        {
            return Move.R;
        }

        public Move Water()
        {
            return Move.W;
        }

        public Move Dynamite()
        {
            return Move.D;
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
