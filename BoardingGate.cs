using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268975C_PRG2Assignment
{
    public class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }
        public BoardingGate() { }
        public BoardingGate(string gn, bool cfft, bool ddjb, bool lwtt)
        {
            GateName = gn;
            SupportsCFFT = cfft;
            SupportsDDJB = ddjb;
            SupportsLWTT = lwtt;
        }
        public double CalculateFees()
        {
            return 300 + Flight.CalculateFees();
        }
        public override string ToString()
        {
            return $"Gate Name: {GateName}";
        }
    }
}
