using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268975C_PRG2Assignment
{
    public class LWTTFlight: Flight
    {
        public LWTTFlight(string fn, string o, string d, DateTime et, string s)
        : base(fn, o, d, et, s)
        {
            FlightNumber = fn;
            Origin = o;
            Destination = d;
            ExpectedTime = et;
            Status = s;
        }
        double totalfees = 0;
        public override double CalculateFees()
        {
            if (Origin == "Singapore (SIN)")
            {
                /* Adds all fees for arrival */
                totalfees += 1300;
                return totalfees;
            }
            if (Destination == "Singapore (SIN)")
            {
                /* Adds all fees for departure */
                totalfees += 1600;
                return totalfees;
            }
            return 0;
        }
        public override string ToString()
        {
            return "Fees added for LWTTF special code flight successfully";
        }
    }
}
