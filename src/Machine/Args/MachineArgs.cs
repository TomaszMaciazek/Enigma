using System;
using System.Collections.Generic;
using System.Text;

namespace Machine.Args
{
    public class MachineArgs
    {
        public RotorArgs RotorOneArgs { get; set; }
        public RotorArgs RotorTwoArgs { get; set; }
        public RotorArgs RotorThreeArgs { get; set; }
        public IEnumerable<Tuple<char, char>> LettersToSwap { get; set; }
    }
}
