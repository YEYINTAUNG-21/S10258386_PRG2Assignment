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
    public class DDJBFlight: Flight
    {
        public DDJBFlight(string fn, string o, string d, DateTime et)
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
                /* Adds all fees for arrival */
                totalfees += 1100;
                return totalfees;
            }
            if (Destination == "Singapore (SIN)")
            {
                /* Adds all fees for departure*/
                totalfees += 1400;
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
