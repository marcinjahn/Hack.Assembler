namespace HackAssembler.Lib.Services
{
    public class VariableAddressGenerator
    {
        private int _lastAddress = 15;
        public int GenerateNew()
        {
            return ++_lastAddress;
        }
    }
}