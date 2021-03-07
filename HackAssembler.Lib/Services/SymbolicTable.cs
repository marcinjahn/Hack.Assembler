using System.Collections.Generic;

namespace HackAssembler.Lib.Services
{
    public class SymbolicTable
    {
        public SymbolicTable()
        {
            InitializeWithDefaults();
        }

        private void InitializeWithDefaults()
        {
            _map.Add("R0", 0);
            _map.Add("R1", 1);
            _map.Add("R2", 2);
            _map.Add("R3", 3);
            _map.Add("R4", 4);
            _map.Add("R5", 5);
            _map.Add("R6", 6);
            _map.Add("R7", 7);
            _map.Add("R8", 8);
            _map.Add("R9", 9);
            _map.Add("R10", 10);
            _map.Add("R11", 11);
            _map.Add("R12", 12);
            _map.Add("R13", 13);
            _map.Add("R14", 14);
            _map.Add("R15", 15);
            
            _map.Add("SP", 0);
            _map.Add("LCL", 1);
            _map.Add("ARG", 2);
            _map.Add("THIS", 3);
            _map.Add("THAT", 4);
            
            _map.Add("SCREEN", 16384);
            _map.Add("KBD", 24576);
        }
        
        private readonly IDictionary<string, int> _map = new Dictionary<string, int>();
        public bool Contains(string symbol)
        {
            return _map.ContainsKey(symbol);
        }

        public int GetAddress(string symbol)
        {
            return _map[symbol];
        }

        public void Add(string symbol, int address)
        {
            _map.Add(symbol, address);
        }
    }
}