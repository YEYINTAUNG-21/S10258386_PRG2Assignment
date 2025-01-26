//==========================================================
// Student Number	: S10258386B
// Student Name	: Arjun Vivekananthan
// Partner Name	: Ye Yint Aung
//==========================================================

using S10258386_PRG2Assignment;

Terminal terminal = new Terminal { TerminalName = "Terminal 5" };
try
{
    /* basic feature 1 */
    Console.WriteLine("Loading airlines...");
    using (StreamReader reader = new StreamReader("airlines.csv"))
    {
        reader.ReadLine();
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            var parts = line.Split(',');
            string airlineName = parts[0].Trim();
            string airlineCode = parts[1].Trim();
            terminal.Airlines.Add(airlineCode, new Airline(airlineName, airlineCode));
        }
    }
    Console.WriteLine($"{terminal.Airlines.Count} Airlines Loaded!");

    Console.WriteLine("Loading boarding gates...");
    using (StreamReader reader = new StreamReader("boardinggates.csv"))
    {
        reader.ReadLine();
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            var parts = line.Split(',');
            string boardingGate = parts[0].Trim();
            bool supportsCFFT = parts[1].Trim() == "True";
            bool supportsDDJB = parts[2].Trim() == "True";
            bool supportsLWTT = parts[3].Trim() == "True";
            BoardingGate gate = new BoardingGate(boardingGate, supportsCFFT, supportsDDJB, supportsLWTT);
            terminal.BoardingGates.Add(boardingGate, gate);
        }
    }
    Console.WriteLine($"{terminal.BoardingGates.Count} Boarding Gates Loaded!");


   /* basic feature 2 */

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

void ListAllFlights()
{
    Console.WriteLine("{0, -14} {1, -19} {2, -19} {3, -17} {4, -27}",
        "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");

    foreach (var flight in terminal.Flights.Values)
    {
      
    }
}

void ListBoardingGates()
{

}

void AssignBoardingGate()
{

}

void CreateFlight()
{

}

void DisplayAirlineFlights()
{

}

void ModifyFlightDetails()
{

}

void DisplayFlightSchedule()
{

}

while (true)
{
    ShowMenu();
    Console.WriteLine("Please select your option: ");
    string option = Console.ReadLine();
    if (option == "0")
    {
        Console.WriteLine("Bye Bye!");
        break;
    }
    else if (option == "1")
    {
        ListAllFlights();
    }
    else if (option == "2")
    {
        ListBoardingGates();
    }
    else if (option == "3")
    {
        AssignBoardingGate();
    }
    else if (option == "4")
    {
        CreateFlight();
    }
    else if (option == "5")
    {
        DisplayAirlineFlights();
    }
    else if (option == "6")
    {
        ModifyFlightDetails();
    }
    else if (option == "7")
    {
        DisplayFlightSchedule();
    }
    else
    {
        Console.WriteLine("Invalid Option!");
    }
}