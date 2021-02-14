using System.Linq;

namespace Machine.Args
{
    public class Plugboard
    {
        public string Letters { get; set; }

        public Plugboard()
        {
            Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }

        public void SwapLettersInPlugBoard(char first, char second)
        {
            var swapResult = Letters.Select(c => c == first ? second : (c == second ? first : c)).ToArray();
            Letters = new string(swapResult);
        }

        public char EncodeCharacter(char character) => Letters[character - 65];
    }
}
