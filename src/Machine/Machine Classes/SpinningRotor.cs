using Machine.Args;
using Machine.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Machine.Machine_Classes
{
    public class SpinningRotor
    {
        private string letters;
        public byte CurrentPosition { get; private set; }
        public char SpinPoint { get; private set; }

        public SpinningRotor(RotorNumber number, byte position)
        {
            SetConfiguration(number, position);
        }

        public void SetConfiguration(RotorNumber number, byte position)
        {
            if(position > 25)
            {
                throw new ForbiddenParameterValueException();
            }
            switch (number)
            {
                case RotorNumber.One:
                    letters = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
                    SpinPoint = 'R';
                    break;
                case RotorNumber.Two:
                    letters = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
                    SpinPoint = 'F';
                    break;
                case RotorNumber.Three:
                    letters = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
                    SpinPoint = 'W';
                    break;
                case RotorNumber.Four:
                    letters = "ESOVPZJAYQUIRHXLNFTGKDCMWB";
                    SpinPoint = 'K';
                    break;
                case RotorNumber.Five:
                    letters = "VZBRGITYUPSDNHLXAWMJQOFECK";
                    SpinPoint = 'A';
                    break;
            }
            CurrentPosition = position;
        }

        public void EncodeCharacterFront(ref char characterToEncode)
        {

            characterToEncode = letters[(characterToEncode - 65 + CurrentPosition) % 26];
            characterToEncode = (char)(65 + (characterToEncode - 39 - CurrentPosition) % 26);
        }

        public void EncodeCharacterBack(ref char characterToEncode)
        {
            characterToEncode = (char)(65 + (characterToEncode - 65 + CurrentPosition) % 26);
            int positionOfCharacter = (letters.IndexOf(characterToEncode));
            characterToEncode = (char)(65 + (26 + positionOfCharacter - CurrentPosition) % 26);
        }

        public void SpinRotor()
        {
            CurrentPosition = CurrentPosition == 25 ? CurrentPosition = 0 : ++CurrentPosition;
        }
    }
}
