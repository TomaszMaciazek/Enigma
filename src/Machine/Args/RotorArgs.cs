namespace Machine.Args
{

    public enum RotorNumber
    {
        One,
        Two,
        Three,
        Four,
        Five
    }
    public class RotorArgs
    {
        public RotorNumber Number { get; set; }
        public byte StartPosition { get; set; }
    }
}
