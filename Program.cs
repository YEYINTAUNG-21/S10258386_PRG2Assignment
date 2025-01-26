//==========================================================
// Student Number	: S10268975C
// Student Name	: YE YINT AUNG
// Partner Name	: Arjun Vivekananthan
//==========================================================

using S10268975C_PRG2Assignment;

Terminal terminal = new Terminal { TerminalName = "Terminal 5" };

/* basic feature 1 */
try
{
    Console.WriteLine("Loading airlines...");
    using (StreamReader reader = new StreamReader("airlines.csv"))
    {
        reader.ReadLine();
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            terminal.Airlines.Add(parts[0].Trim() = new Airline { Name = parts[0].Trim(), Code = parts[1].Trim() };)
        }
    }
    Console.WriteLine($"{terminal.Airlines.Count} Airlines Loaded!");
}
catch(Exception ex)
{ Console.WriteLine(ex.Message); }

try
{
    Console.WriteLine("Loading boarding gates...");
    using (StreamReader reader = new StreamReader("boardinggates.csv"))
    {
        reader.ReadLine();
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            terminal.BoardingGates.Add(parts[0].Trim() = new Airline { GateName = parts[0].Trim(), SupportsCFFT = bool.Parse(parts[1].Trim()), SupportsDDJB = bool.Parse(parts[2].Trim()), SupportsLWTT  = bool.Parse(parts[3].Trim()) };)
        }
    }
    Console.WriteLine($"{terminal.Airlines.Count} Airlines Loaded!");
}
catch (Exception ex)
{ Console.WriteLine(ex.Message); }


\/* basic feature 2 */
try
{
    Console.WriteLine("Loading flights...");
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        sr.ReadLine();
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            string[] data = line.Split(",");
            string flightNumber = data[0].Trim();
            string origin = data[1].Trim();
            string destination = data[2].Trim();
            DateTime expectedTime = DateTime.Parse(data[3].Trim());
            string specialResquestCode = data[4].Trim();
            Flight flight;
            if(specialResquestCode == "CFFT")
            {
                flight = new CFFTFlight(flightNumber, origin, destination, expectedTime);
            }
            else if(specialResquestCode == "DDJB")
            {
                flight = new DDJBFlight(flightNumber, origin, destination, expectedTime);
            }
            else if(specialResquestCode == "LWTTF")
            {
                flight = new LWTTFlight(flightNumber, origin, destination, expectedTime);
            }
            else
            {
                flight = new NORMFlight(flightNumber, origin, destination, expectedTime);
            }
            terminal.Flights.Add(flightNumber, flight);
        }
    }
    Console.WriteLine($"{terminal.Flights.Count()} Flights Loaded!");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    return;
}

    void ShowMenu()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("0. Exit");
}
