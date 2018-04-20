using BotInterface.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace makeitwork
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DoesntErrorOnRoundOne()
        {
            var state = new Gamestate();
            state.SetRounds(new Round[]{});
            var bot = new ExampleBot.ExampleBot();

            bot.MakeMove(state);
        }

        [TestMethod]
        public void RoundTwoTest()
        {
            // round 1
            var state = new Gamestate();
            state.SetRounds(new Round[] { });
            var bot = new ExampleBot.ExampleBot();

            bot.MakeMove(state);

            // round 2
            var round1 = new Round();
            round1.SetP1(Move.D);
            round1.SetP2(Move.D);
            state.SetRounds(new[]
            {
                round1
            });

            bot.MakeMove(state);
        }

        [TestMethod]
        public void FuckingTest()
        {
            var round1 = new Round();
            round1.SetP1(Move.R);
            round1.SetP2(Move.P);
            var round2 = new Round();
            round2.SetP1(Move.R);
            round2.SetP2(Move.P);
            var state = new Gamestate();
            state.SetRounds(new Round[] { round1, round2 });

            var result = new ExampleBot.ExampleBot().MakeMove(state);

            Assert.AreEqual(Move.S, result);
        }
    }
}
