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
    public abstract class Flight: IComparable<Flight>
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; } = "Schduled";
        public string AssignedBoardingGate { get; set; } = "Unassigned"; // For Advanced feature a
        public string specialRequestCode { get; set; } = "None"; // For Advanced feature a
        public Flight() { }
        public Flight(string fn, string o, string d, DateTime et, string s, string abg, string src)
        {
            FlightNumber = fn;
            Origin = o;
            Destination = d;
            ExpectedTime = et;
            Status = "Schduled";
            AssignedBoardingGate = "Unassigned"; // For Advanced feature a
            specialResquestCode = src; // For Advanced feature a
        }
        public abstract double CalculateFees();
        public int CompareTo(Flight other)
        {
            return this.ExpectedTime.CompareTo(other.ExpectedTime);
        }
        public override string ToString()
        {
            return $"Flight Number: {FlightNumber}, Origin: {Origin}, Destination: {Destination}, Expected Time: {ExpectedTime}, Status: {Status}";
        }
    }
}
