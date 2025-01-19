using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268975C_PRG2Assignment
{
    public class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Airline() { }
        public Airline(string n, string c)
        {
            Name = n;
            Code = c;
        }
        public bool AddFlight(Flight f)
        {
            if (Flights.ContainsKey(f.FlightNumber))
            {
                return false;
            }
            Flights.Add(f.FlightNumber, f);
            return true;
        }

    }
}
