using Machine.Args;

namespace Machine.Interfaces
{
    public interface IEnigmaMachine
    {
        void ConfigureMachine(MachineArgs args);
        string EncodeString(string stringToEncode);
    }
}