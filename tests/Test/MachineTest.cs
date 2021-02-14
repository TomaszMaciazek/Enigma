using Machine.Args;
using Machine.Interfaces;
using Machine.Machine_Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestFixture]
    public class MachineTest
    {
        private IEnigmaMachine _machine;

        [SetUp]
        public void SetUp()
        {
            _machine = new EnigmaMachine();
        }

        [TestCaseSource("TestCasesEncode")]
        public void EncodeStringTest(MachineArgs args, string first, string second)
        {
            _machine.ConfigureMachine(args);
            Assert.AreEqual(second, _machine.EncodeString(first));
            _machine.ConfigureMachine(args);
            Assert.AreEqual(first, _machine.EncodeString(second));
        }

        public static IEnumerable<TestCaseData> TestCasesEncode
        {
            get
            {
                yield return new TestCaseData(new MachineArgs {
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
                },"ABCDEFG", "EKDTDWB"
                );
                yield return new TestCaseData(new MachineArgs
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
                },"ALAXMAXKOTA", "SDJSDNQWFZJ");
                yield return new TestCaseData(new MachineArgs
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
                        new Tuple<char, char>('P','E')
                    }
                }, "TOMEKXPIEKLXCHLEBXWXPIATEK", "ZTYNBTMNFEAAFPXYRBIHHYCNID");
            }
        }
    }
}
