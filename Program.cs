//==========================================================
// Student Number	: S10258386B
// Student Name	: Arjun Vivekananthan
// Partner Name	: Ye Yint Aung
//==========================================================

using System.Net;
using System.Reflection.Metadata.Ecma335;
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


   /* basic feature 2 (YE YINT AUNG)*/

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

/* basic feature 3 (YE YINT AUNG) */
void ListAllFlights()
{
    try
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Welcome to Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("{0, -14} {1, -19} {2, -19} {3, -17} {4, -27}",
        "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");

        foreach (var flight in terminal.Flights.Values)
        {
            string flightNumber = flight.FlightNumber;
            string origin = flight.Origin;
            string destination = flight.Destination;
            DateTime expectedTime = flight.ExpectedTime;
            string airlineName = "";
            foreach (var airline in terminal.Airlines.Values)
            {
                if (airline.Code == flightNumber.Substring(0, 2))
                {
                    airlineName = airline.Name;
                    break;
                }
            }
            Console.WriteLine("{0, -14} {1, -19} {2, -19} {3, -17} {4, -27}",
                    flightNumber, airlineName, origin, destination, expectedTime);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

void ListBoardingGates()
{

}

/* basic feature 5 (YE YINT AUNG) */
void AssignBoardingGate()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Assign a Boarding Gate to a Flight");
    Console.WriteLine("=============================================");
    Console.WriteLine("Enter Flight Number: ");
    string flightNumber = Console.ReadLine();
    if (!terminal.Flights.ContainsKey(flightNumber))
    {
        Console.WriteLine("Flight not found!");
        return;
    }
    Flight flight = terminal.Flights[flightNumber];

    Console.WriteLine("Enter Boarding Gate Name:");
    string boardingGateName = Console.ReadLine();

    if (!terminal.BoardingGates.ContainsKey(boardingGateName))
    {
        Console.WriteLine("Boarding Gate not found!");
        return;
    }

    BoardingGate boardingGate = terminal.BoardingGates[boardingGateName];

    if (boardingGate.Flight != null)
    {
        Console.WriteLine("Boarding Gate already assigned to a flight!");
        return;
    }
    boardingGate.Flight = flight;
    Console.WriteLine($"Flight Number: {flight.FlightNumber}");
    Console.WriteLine($"Orign: {flight.Origin}");
    Console.WriteLine($"Destination: {flight.Destination}");
    Console.WriteLine($"Expected Time: {flight.ExpectedTime}");
    string specaialRequestCode = "";
    if (flight is CFFTFlight)
    {
        specaialRequestCode = "CFFT";
    }
    else if (flight is DDJBFlight)
    {
        specaialRequestCode = "DDJB";
    }
    else if (flight is LWTTFlight)
    {
        specaialRequestCode = "LWTTF";
    }
    else
    {
        specaialRequestCode = "None";
    }
    Console.WriteLine($"Special Resquest Code: {specaialRequestCode}");
    Console.WriteLine($"Boarding Gate Name: {boardingGateName}");
    Console.WriteLine($"Supports DDJB: {boardingGate.SupportsDDJB}");
    Console.WriteLine($"Supports CFFT: {boardingGate.SupportsCFFT}");
    Console.WriteLine($"Supports LWTT: {boardingGate.SupportsLWTT}");
    Console.WriteLine($"Would you like to update the status of the flight? (Y/N)");
    string response = Console.ReadLine();
    if (response.ToUpper() == "Y")
    {
        Console.WriteLine("1. Delayed");
        Console.WriteLine("2. Boarding");
        Console.WriteLine("3. On Time");
        Console.WriteLine("Please select the new status of the flight:");
        string choice = Console.ReadLine();
        if (choice == "1")
        {
            flight.Status = "Delayed";
        }
        else if (choice == "2")
        {
            flight.Status = "Boarding";
        }
        else if (choice == "3")
        {
            flight.Status = "On Time";
        }
        else
        {
            Console.WriteLine("Invalid Option!");
        }
    }
    else
    {
        flight.Status = "On Time";
    }
    Console.WriteLine($"Flight {flight.FlightNumber} has been assigned to Boarding Gate {boardingGate.GateName}!");
}

/* basic feature 6 (YE YINT AUNG) */
void CreateFlight()
{
    while (true)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Create Flight");
        Console.WriteLine("=============================================");
        Console.WriteLine("Enter new Flight Number:");
        string flightNumber = Console.ReadLine();
        if (terminal.Flights.ContainsKey(flightNumber))
        {
            Console.WriteLine("Flight already exists!");
            return;
        }
        Console.WriteLine("Enter Origin:");
        string origin = Console.ReadLine();
        Console.WriteLine("Enter Destination:");
        string destination = Console.ReadLine();
        Console.WriteLine("Enter Expected Departure/Arrival Time (HH:mm AM/PM):");
        DateTime expectedTime = DateTime.Parse(Console.ReadLine().Trim());
        Console.WriteLine("Do you want to add addition information? [Y/N]:");
        string response = Console.ReadLine();
        Flight flight;
        string specialResquestCode = "";
        if (response.ToUpper() == "Y")
        {
            Console.WriteLine("1. CFFT");
            Console.WriteLine("2. DDJB");
            Console.WriteLine("3. LWTT");
            Console.WriteLine("Please select the special request code:");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                flight = new CFFTFlight(flightNumber, origin, destination, expectedTime);
                specialResquestCode = "CFFT";
            }
            else if (choice == "2")
            {
                flight = new DDJBFlight(flightNumber, origin, destination, expectedTime);
                specialResquestCode = "DDJB";
            }
            else if (choice == "3")
            {
                flight = new LWTTFlight(flightNumber, origin, destination, expectedTime);
                specialResquestCode = "LWTT";
            }
            else
            {
                Console.WriteLine("Invalid Option!");
                return;
            }
        }
        else
        {
            flight = new NORMFlight(flightNumber, origin, destination, expectedTime);
        }
        terminal.Flights.Add(flightNumber, flight);

        try
        {
            Console.WriteLine($"Writing flight: {flightNumber} | {origin} | {destination} | {expectedTime:hh:mm tt} | {specialResquestCode}");
            using (StreamWriter sw = new StreamWriter("flights.csv", true))
            {
                string formattedTime = expectedTime.ToString("h:mm tt");
                sw.WriteLine($"{flightNumber},{origin},{destination},{formattedTime},{specialResquestCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine($"Flight Number {flightNumber} has been successfully added!");

        Console.WriteLine("Would you like to add another flight? [Y/N]:");
        string anotherChoice = Console.ReadLine();
        if (anotherChoice.ToUpper() == "N")
        {
            break;
        }
    }
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