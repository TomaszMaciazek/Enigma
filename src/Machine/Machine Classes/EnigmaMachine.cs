using Machine.Args;
using Machine.Interfaces;
using System.Text;

namespace Machine.Machine_Classes
{
    public class EnigmaMachine : IEnigmaMachine
    {
        private const string _invertingRotor = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
        private readonly SpinningRotor[] _rotors;
        private readonly Plugboard _plugboard;
        public EnigmaMachine()
        {
            _rotors = new SpinningRotor[3];
            _plugboard = new Plugboard();
        }

        public void ConfigureMachine(MachineArgs args)
        {

            _rotors[0] = new SpinningRotor(args.RotorOneArgs.Number, args.RotorOneArgs.StartPosition);
            _rotors[1] = new SpinningRotor(args.RotorTwoArgs.Number, args.RotorTwoArgs.StartPosition);
            _rotors[2] = new SpinningRotor(args.RotorThreeArgs.Number, args.RotorThreeArgs.StartPosition);
            foreach (var pair in args.LettersToSwap)
            {
                _plugboard.SwapLettersInPlugBoard(pair.Item1, pair.Item2);
            }
        }

        public string EncodeString(string stringToEncode)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var c in stringToEncode)
            {
                char encodedCharacter = EncodeCharacter(c);
                builder.Append(encodedCharacter);
            }
            return builder.ToString();
        }

        private char EncodeCharacter(char character)
        {
            SpinRotors();
            char encodedCharacter = _plugboard.EncodeCharacter(character);
            for (int i = 2; i >= 0; i--)
            {
                _rotors[i].EncodeCharacterFront(ref encodedCharacter);
            }
            encodedCharacter = _invertingRotor[encodedCharacter - 65];
            for (int i = 0; i <= 2; i++)
            {
                _rotors[i].EncodeCharacterBack(ref encodedCharacter);
            }
            return _plugboard.EncodeCharacter(encodedCharacter);
        }

        private void SpinRotors()
        {
            bool willNextOneSpin = true;
            for (int j = 2; j >= 0; j--)
            {
                willNextOneSpin = 65 + _rotors[j].CurrentPosition == _rotors[j].SpinPoint;
                _rotors[j].SpinRotor();
                if (!willNextOneSpin)
                {
                    break;
                }
            }
        }
    }
}
