using S10268975C_PRG2Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace S10258386_PRG2Assignment
{
    public class NORMFlight(string origin, string destination, double totalfees) : Flight
    {
        public override double CalculateFees()
        {
           if (origin == "Singapore (SIN)")
            {
                /* Adds both boarding gate base fee and Arriving flight fee */
                totalfees += 800;
                return totalfees;
            }
           if (destination == "Singapore (SIN)")
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
