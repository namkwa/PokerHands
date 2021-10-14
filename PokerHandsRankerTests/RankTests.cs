using NFluent;
using NUnit.Framework;
using PokerHandsRanker;

namespace PokerHandsRankerTests
{
    public class RankTests
    {
        [Test]
        public void Should_Have_Rank1_Better_Than_Rank2_When_Higher()
        {
            Rank rank1 = new Rank(3, "");
            Rank rank2 = new Rank(2, "");
            Check.That(rank1.IsBetterRank(rank2)).IsEqualTo(true);
        }

        [Test]
        public void Should_Have_Rank2_Better_Than_Rank1_When_Lower()
        {
            Rank rank1 = new Rank(2, "");
            Rank rank2 = new Rank(3, "");
            Check.That(rank1.IsBetterRank(rank2)).IsEqualTo(false);
        }

        [Test]
        public void Should_Have_Rank1_Better_Than_Rank2_When_Same_Rank_But_Higher_Card()
        {
            Rank rank1 = new Rank(2, "8C");
            Rank rank2 = new Rank(2, "7H");
            Check.That(rank1.IsBetterRank(rank2)).IsEqualTo(true);
        }

        [Test]
        public void Should_Have_A_Tie_When_Ranks_Are_The_Same()
        {
            Rank rank1 = new Rank(2, "8C");
            Rank rank2 = new Rank(2, "8H");
            Check.That(rank1.IsBetterRank(rank2)).IsEqualTo(null);
        }
    }
}
