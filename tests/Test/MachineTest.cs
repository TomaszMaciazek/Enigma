using Machine.Args;
using Machine.Interfaces;
using Machine.Machine_Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Test
{
    [TestFixture]
    public class MachineTest
    {
        private IEnigmaMachine _machine;
        private IList<MachineArgs> _args;
        [SetUp]
        public void SetUp()
        {
            _machine = new EnigmaMachine();
            _args = new List<MachineArgs>()
            {
                new MachineArgs {
                    RotorOneArgs = new RotorArgs
                    {
                        Number = RotorNumber.One,
                        StartPosition = 0
                    },
                    RotorTwoArgs = new RotorArgs
                    {
                        Number = RotorNumber.Two,
                        StartPosition = 5
                    },
                    RotorThreeArgs = new RotorArgs
                    {
                        Number = RotorNumber.Three,
                        StartPosition = 15
                    },
                    LettersToSwap = new List<Tuple<char,char>>()
                },
                new MachineArgs
                {
                    RotorOneArgs = new RotorArgs
                    {
                        Number = RotorNumber.Five,
                        StartPosition = 6
                    },
                    RotorTwoArgs = new RotorArgs
                    {
                        Number = RotorNumber.Four,
                        StartPosition = 9
                    },
                    RotorThreeArgs = new RotorArgs
                    {
                        Number = RotorNumber.Three,
                        StartPosition = 1
                    },
                    LettersToSwap = new List<Tuple<char, char>>() {
                        new Tuple<char, char>('A','E'),
                        new Tuple<char, char>('G','K')
                    }
                },
                new MachineArgs
                {
                    RotorOneArgs = new RotorArgs
                    {
                        Number = RotorNumber.Four,
                        StartPosition = 14
                    },
                    RotorTwoArgs = new RotorArgs
                    {
                        Number = RotorNumber.Two,
                        StartPosition = 24
                    },
                    RotorThreeArgs = new RotorArgs
                    {
                        Number = RotorNumber.Four,
                        StartPosition = 17
                    },
                    LettersToSwap = new List<Tuple<char, char>>() {
                        new Tuple<char, char>('C','F'),
                        new Tuple<char, char>('E','T'),
                        new Tuple<char, char>('P','L')
                    }
                }
            };
        }

        [TestCase(0, "ABCDEFG", "EKDTDWB")]
        [TestCase(0, "EKDTDWB", "ABCDEFG")]
        [TestCase(1, "ALAXMAXKOTA", "UUYFIMBFYWP")]
        [TestCase(1, "UUYFIMBFYWP", "ALAXMAXKOTA")]
        [TestCase(2, "TOMEKXPIEKLXCHLEBXWXPIATEK", "SEYWBEHNVTJAFLBSRBIHSYFBFD")]
        [TestCase(2, "SEYWBEHNVTJAFLBSRBIHSYFBFD", "TOMEKXPIEKLXCHLEBXWXPIATEK")]
        public void EncodeStringTest(int argsIndex, string first, string second)
        {
            _machine.ConfigureMachine(_args[argsIndex]);
            Assert.AreEqual(second, _machine.EncodeString(first));
        }
    }
}
