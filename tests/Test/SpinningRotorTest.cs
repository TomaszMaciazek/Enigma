using Machine.Args;
using Machine.Exceptions;
using Machine.Machine_Classes;
using NUnit.Framework;
using System.Collections.Generic;

namespace Test
{
    [TestFixture]
    public class SpinningRotorTest
    {
        public static IEnumerable<TestCaseData> TestCasesEncodeFront
        {
            get
            {
                yield return new TestCaseData(RotorNumber.One, (byte)0, 'A', 'E');
                yield return new TestCaseData(RotorNumber.Two, (byte)2, 'E', 'P');
                yield return new TestCaseData(RotorNumber.Three, (byte)4, 'P', 'W');
                yield return new TestCaseData(RotorNumber.Four, (byte)10, 'Z', 'G');
                yield return new TestCaseData(RotorNumber.Five, (byte)7, 'G', 'A');
                yield return new TestCaseData(RotorNumber.Three, (byte)17, 'A', 'F');
                yield return new TestCaseData(RotorNumber.One, (byte)7, 'H', 'R');
            }
        }

        public static IEnumerable<TestCaseData> TestCasesEncodeBack
        {
            get
            {
                yield return new TestCaseData(RotorNumber.One, (byte)7, 'B', 'O');
                yield return new TestCaseData(RotorNumber.Two, (byte)13, 'O', 'W');
                yield return new TestCaseData(RotorNumber.Three, (byte)10, 'W', 'I');
                yield return new TestCaseData(RotorNumber.Four, (byte)24, 'E', 'Y');
                yield return new TestCaseData(RotorNumber.Five, (byte)12, 'O', 'E');
                yield return new TestCaseData(RotorNumber.Three, (byte)20, 'V', 'N');
                yield return new TestCaseData(RotorNumber.Two, (byte)15, 'Y', 'E');
            }
        }

        [TestCaseSource("TestCasesEncodeFront")]
        public void EncodeFrontTest(RotorNumber number, byte position, char characterToEncode, char result)
        {
            SpinningRotor rotor = new SpinningRotor(number, position);
            rotor.EncodeCharacterFront(ref characterToEncode);
            Assert.AreEqual(result, characterToEncode);
        }

        [TestCaseSource("TestCasesEncodeBack")]
        public void EncodeBackTest(RotorNumber number, byte position, char characterToEncode, char result)
        {
            SpinningRotor rotor = new SpinningRotor(number, position);
            rotor.EncodeCharacterBack(ref characterToEncode);
            Assert.AreEqual(result, characterToEncode);
        }

        [TestCase(4, 5)]
        [TestCase(25, 0)]
        public void SpinRotorTest(byte position, byte result)
        {
            SpinningRotor rotor = new SpinningRotor(RotorNumber.One, position);
            rotor.SpinRotor();
            Assert.AreEqual(result, rotor.CurrentPosition);
        }

        [Test]
        public void ForbidenValueInConfigurationTest()
        {
            Assert.Throws<ForbiddenParameterValueException>(() 
                => new SpinningRotor(RotorNumber.One, 26));
        }
    }
}
