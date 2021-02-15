using Machine.Machine_Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Test
{
    [TestFixture]
    public class PlugboardTest
    {
        private Plugboard _plugboard;

        [SetUp]
        public void SetUp()
        {
            _plugboard = new Plugboard();
        }

        public static IEnumerable<TestCaseData> SwapCharactersTestData
        {
            get
            {
                yield return new TestCaseData(new List<Tuple<char, char>>(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
                yield return new TestCaseData(new List<Tuple<char, char>>() {
                    new Tuple<char, char>('A','B')
                    },
                    "BACDEFGHIJKLMNOPQRSTUVWXYZ");
                yield return new TestCaseData(new List<Tuple<char, char>>() {
                    new Tuple<char, char>('C','F'), new Tuple<char, char>('H','G')
                    },
                    "ABFDECHGIJKLMNOPQRSTUVWXYZ");
                yield return new TestCaseData(new List<Tuple<char, char>>() {
                    new Tuple<char, char>('D','X'), new Tuple<char, char>('J','L'), new Tuple<char, char>('Z','A')
                    },
                    "ZBCXEFGHILKJMNOPQRSTUVWDYA");
            }
        }

        [TestCaseSource("SwapCharactersTestData")]
        public void SwapCharactersTest(IEnumerable<Tuple<char, char>> pairs, string result)
        {
            foreach (var pair in pairs)
            {
                _plugboard.SwapLettersInPlugBoard(pair.Item1, pair.Item2);
            }
            Assert.AreEqual(result, _plugboard.Letters);
        }
    }
}
