using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10258386B
// Student Name : Arjun Vivekananthan
// Partner Name : Ye Yint Aung
//==========================================================

namespace S10258386_PRG2Assignment
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
        public double CalculateFees()
        {
            double totalFees = 0;
            foreach (var flight in Flights.Values)
            {
                totalFees += flight.CalculateFees();
            }
            return totalFees;
        }
        public bool RemoveFlight(Flight flight)
        {
            return Flights.Remove(flight.FlightNumber);
        }
        public override string ToString()
        {
            return $"Airline Name: {Name}, Airline Code: {Code}";
        }
    }
}
