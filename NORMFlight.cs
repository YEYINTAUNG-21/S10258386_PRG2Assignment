using S10258386_PRG2Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace S10258386_PRG2Assignment
{
    public class NORMFlight : Flight
    {
        public NORMFlight(string fn, string o, string d, DateTime et)
        : base(fn, o, d, et, "Schduled")
        {
            {
                FlightNumber = fn;
                Origin = o;
                Destination = d;
                ExpectedTime = et;
            }
            
        }
        double totalfees = 0;
        public override double CalculateFees()
        {
           if (Origin == "Singapore (SIN)")
            {
                /* Adds both boarding gate base fee and Arriving flight fee */
                totalfees += 800;
                return totalfees;
            }
           if (Destination == "Singapore (SIN)")
            {
                /* Adds both boarding gate base fee and Departing flight fee */
                totalfees += 1100;
                return totalfees;
            }
            return 0;
        }
        public override string ToString()
        {
            return "Fees Added for NORM flight Successfully";
        }
    }
}
