using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10258386_PRG2Assignment
{
    public class CFFTFlight : Flight
    {
        public CFFTFlight(string fn, string o, string d, DateTime et)
        : base(fn, o, d, et, "Schduled")
        {
            FlightNumber = fn;
            Origin = o;
            Destination = d;
            ExpectedTime = et;
        }
        double totalfees = 0;
        public override double CalculateFees()
        {
            if (Origin == "Singapore (SIN)")
            {
                /* Adds all the fees for arrival flight */
                totalfees += 950;
                return totalfees;
            }
            if (Destination == "Singapore (SIN)")
            {
                /* Adds all the fees for departing flight*/
                totalfees += 1250;
                return totalfees;
            }
            return 0;
        }
        public override string ToString()
        {
            return "Fees added for DDJB special code flight successfully";
        }
    }
}
