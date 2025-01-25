using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268975C_PRG2Assignment
{
    public class CFFTFlight(string origin, string destination, double totalfees) : Flight
    {
        /* Doesnt need a constructor as there is no need for it */
        public double requestfee = 150;
        public override double CalculateFees()
        {
            if (origin == "Singapore (SIN)")
            {
                /* Adds both boarding gate base fee and Arriving flight fee */
                totalfees += 800;
                /* Adds the request fee for the special code */
                totalfees += requestfee;
                return totalfees;
            }
            if (destination == "Singapore (SIN)")
            {
                /* Adds both boarding gate base fee and Departing flight fee */
                totalfees += 1100;
                /* Adds the request fee for the special code */
                totalfees += requestfee;
                return totalfees;
            }
            return 0;
        }
        public override string ToString()
        {
            return "Fees Added for CFFT flight Successfully";
        }
    }
}
