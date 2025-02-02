//==========================================================
// Student Number	: S10258386B
// Student Name	: Arjun Vivekananthan
// Partner Name	: Ye Yint Aung
//==========================================================

using System;
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
            string specialRequestCode = data[4].Trim();
            Flight flight;
            if (specialRequestCode == "CFFT")
            {
                flight = new CFFTFlight(flightNumber, origin, destination, expectedTime);
            }
            else if (specialRequestCode == "DDJB")
            {
                flight = new DDJBFlight(flightNumber, origin, destination, expectedTime);
            }
            else if (specialRequestCode == "LWTTF")
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
    Console.WriteLine("8. Bulk Assign Boarding Gates");
    Console.WriteLine("9. Display Airline Total Fees");
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

/* Basic Feature 4 (ARJUN VIVEKANANTHAN) */
void ListBoardingGates()

    try
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("Gate Name        DDJB                  CFFT                  LWTT");
        foreach (var boardingGate in terminal.BoardingGates.Values)
        {
            string BoardingGateNo = boardingGate.GateName;
            bool DDJB = boardingGate.SupportsDDJB;
            bool CFFT = boardingGate.SupportsCFFT;
            bool LWTT = boardingGate.SupportsLWTT;
            Console.WriteLine("{0, -4} {1, -5} {2, -5} {3, -5}",BoardingGateNo,DDJB,CFFT,LWTT);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

}

/* basic feature 5 (YE YINT AUNG) */
void AssignBoardingGate()
{
    try
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
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

/* basic feature 6 (YE YINT AUNG) */
void CreateFlight()
{
    try
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
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
/* basic feature 7 (ARJUN VIVEKANANTHAN)*/
void DisplayAirlineFlights()
{
    try
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("Airline Code   Airline Name");
        foreach (var airline in terminal.Airlines.Values)
        {
            string airlineCode = airline.Code;
            string airlineName = airline.Name;
            Console.WriteLine($"{airlineCode}             {airlineName}");

        }
        Console.WriteLine("Enter Airline Code:");
        string response = Console.ReadLine();
        foreach (var airline in terminal.Airlines.Values)
        {
            if (airline == null)
            {
                Console.WriteLine("No airlines are assigned to that airline code");
                break;
            }
            if (airline.Code != response.ToUpper())
            {
                continue;
            }
            string airlineName = airline.Name;
            Console.WriteLine("=============================================");
            Console.WriteLine($"List of Flights for {airline.Name}");
            Console.WriteLine("=============================================");
            Console.WriteLine("Flight Number   AirlineName       Origin              Destination         Expected Departure/Arrival Time        Special Request Code    Assigned Boarding Gate");
            foreach (var flight in terminal.Flights.Values)
            {
                string flightcode = flight.FlightNumber.Split(' ')[0];
                if (flightcode == airline.Code)
                {
                    Console.WriteLine($"{flight.FlightNumber}           {airline.Name}      {flight.Origin}             {flight.Destination}        {flight.ExpectedTime}           {flight.specialRequestCode}     {flight.AssignedBoardingGate}");
                }
            }
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

/*basic feature 8 (ARJUN VIVEKANANTHAN) */
void ModifyFlightDetails()
{
    try
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("Airline Code   Airline Name");
        foreach (var airline in terminal.Airlines.Values)
        {
            string airlineCode = airline.Code;
            string airlineName = airline.Name;
            Console.WriteLine($"{airlineCode}             {airlineName}");

        }
        Console.WriteLine("Enter Airline Code:");
        string response = Console.ReadLine();
        foreach (var airline in terminal.Airlines.Values)
        {
            if (airline == null)
            {
                Console.WriteLine("No airlines are assigned to that airline code");
                break;
            }
            if (airline.Code != response.ToUpper())
            {
                continue;
            }
            string airlineName = airline.Name;
            Console.WriteLine("=============================================");
            Console.WriteLine($"List of Flights for {airline.Name}");
            Console.WriteLine("=============================================");
            Console.WriteLine("Flight Number   AirlineName       Origin              Destination         Expected Departure/Arrival Time        Special Request Code    Assigned Boarding Gate");
            foreach (var flight in terminal.Flights.Values)
            {
                string flightcode = flight.FlightNumber.Split(' ')[0];
                if (flightcode == airline.Code)
                {
                    Console.WriteLine($"{flight.FlightNumber}           {airlineName}      {flight.Origin}             {flight.Destination}        {flight.ExpectedTime}");
                }
            }
            Console.WriteLine("Choose an existing flight to modify or delete:");
            string result = Console.ReadLine();
            foreach (var flight in terminal.Flights.Values)
            {
                if (flight == null)
                {
                    Console.WriteLine("No flights were found!");
                    break;
                }
                if (flight.FlightNumber == result.Trim().ToUpper())
                {
                    Console.WriteLine("1. Modify Flight");
                    Console.WriteLine("2. Delete Flight");
                    Console.WriteLine("Choose an option:");
                    string result1 = Console.ReadLine();
                    if (result1.Trim() == "1")
                    {
                        Console.WriteLine("1. Modify Basic Information");
                        Console.WriteLine("2. Modify Status");
                        Console.WriteLine("3. Modify Special Request Code");
                        Console.WriteLine("4. Modify Boarding Gate");
                        string nestedresult = Console.ReadLine();
                        if (nestedresult.Trim() == "1")
                        {
                            Console.WriteLine("Enter new Origin:");
                            string newOrigin = Console.ReadLine();
                            Console.WriteLine("Enter new Destination:");
                            string newDestination = Console.ReadLine();
                            Console.WriteLine("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm:ss):");
                            string newExpectedTime = Console.ReadLine();
                            flight.Origin = newOrigin;
                            flight.Destination = newDestination;
                            flight.ExpectedTime = Convert.ToDateTime(newExpectedTime);
                        }
                        else if (nestedresult.Trim() == "2")
                        {
                            Console.WriteLine("Enter new status:");
                            string statusresult = Console.ReadLine();
                            flight.Status = statusresult;
                        }
                        else if (nestedresult.Trim() == "3")
                        {
                            Console.WriteLine("Enter new Special Request code:");
                            string srcresult = Console.ReadLine();
                            flight.specialRequestCode = srcresult
                        }
                        else if (nestedresult.Trim() == "4")
                        {
                            Console.WriteLine("Enter new Boarding Gate");
                            string gateresult = Console.ReadLine();
                            flight.AssignedBoardingGate = gateresult
                        }
                        Console.WriteLine("Flight updated!");
                        Console.WriteLine($"Flight Number: {flight.FlightNumber}");
                        Console.WriteLine($"Airline Name: {airline.Name}");
                        Console.WriteLine($"Origin: {flight.Origin}");
                        Console.WriteLine($"Destination: {flight.Destination}");
                        Console.WriteLine($"Expected Departure/Arrival Time: {flight.ExpectedTime}");
                        Console.WriteLine($"Status: {flight.Status}");
                        Console.WriteLine($"Special Request Code: {flight.specialRequestCode}");
                        if (flight.AssignedBoardingGate == null)
                        {
                            Console.WriteLine("Boarding Gate: Unassigned");
                        }
                        else
                        {
                            Console.WriteLine($"Boarding Gate: {flight.AssignedBoardingGate}");
                        };
                        Console.WriteLine("Flight Updated!")
                    }
                }
                else
                {
                    continue;
                }
            }
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

/* basic feature 9 (YE YINT AUNG) */
void DisplayFlightSchedule()
{
    try
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("{0, -14} {1, -19} {2, -20} {3, -16} {4, -22} {5, -10} {6, -13}",
            "Flight Number", "Airline Name", "Origin", "Destination", "Expected Time", "Status", "Boarding Gate");
        List<Flight> sortedFlights = terminal.Flights.Values.ToList();
        sortedFlights.Sort();
        foreach (var flight in sortedFlights)
        {
            try
            {
                string flightNumber = flight.FlightNumber;
                string origin = flight.Origin;
                string destination = flight.Destination;
                DateTime expectedTime = flight.ExpectedTime;
                string airlineName = "";
                string boardingGate = "Unassigned";
                foreach (var airline in terminal.Airlines.Values)
                {
                    if (airline.Code == flightNumber.Substring(0, 2))
                    {
                        airlineName = airline.Name;
                        break;
                    }
                }
                foreach (var bg in terminal.BoardingGates.Values)
                {
                    if (bg.Flight == flight)
                    {
                        boardingGate = bg.GateName;
                        break;
                    }
                }
                Console.WriteLine("{0, -14} {1, -19} {2, -20} {3, -16} {4, -22} {5, -10} {6, -13}",
               flight.FlightNumber, airlineName, flight.Origin, flight.Destination, flight.ExpectedTime, flight.Status, boardingGate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

/* Advanced Feature (a) (YE YINT AUNG) */
void BulkAssignBoardingGates()
{
    try
    {
        Console.WriteLine("============================================================");
        Console.WriteLine("Process all unassigned flights to boarding gates in bulk");
        Console.WriteLine("============================================================");

        Queue<Flight> unassignedFlights = new Queue<Flight>();
        foreach (var flight in terminal.Flights.Values)
        {
            if (flight.AssignedBoardingGate == "Unassigned")
            {
                unassignedFlights.Enqueue(flight);
            }
        }
        Console.WriteLine($"The total number of Flights that do not have any Boarding Gate assigned yet: {unassignedFlights.Count()}");

        List<BoardingGate> availableBoardingGates = new List<BoardingGate>();

        foreach (var boardingGate in terminal.BoardingGates.Values)
        {
            if (boardingGate.Flight == null)
            {
                availableBoardingGates.Add(boardingGate);
            }
        }
        Console.WriteLine($"The total number of Boarding Gates that do not have a Flight Number assigned yet: {availableBoardingGates.Count()}");
        int processedFlights = 0;
        while (unassignedFlights.Count() > 0 && availableBoardingGates.Count() > 0)
        {
            Flight flight = unassignedFlights.Dequeue();
            BoardingGate assignGate = null;
            if (flight.specialRequestCode != "None")
            {
                foreach (var boardingGate in availableBoardingGates)
                {
                    if (boardingGate.SupportsSpecialRequestCode(flight.specialRequestCode))
                    {
                        assignGate = boardingGate;
                        break;
                    }
                }
            }
            else
            {
                foreach (var boardingGate in availableBoardingGates)
                {
                    if (!boardingGate.SupportsCFFT && !boardingGate.SupportsDDJB && !boardingGate.SupportsLWTT)
                    {
                        assignGate = boardingGate;
                        break;
                    }
                }
            }
            if (assignGate != null)
            {
                assignGate.Flight = flight;
                availableBoardingGates.Remove(assignGate);
                Console.WriteLine($"Flight Number: {flight.FlightNumber} has been assigned to Boarding Gate: {assignGate.GateName}");
                processedFlights++;
            }
            else
            {
                Console.WriteLine($"No available Boarding Gate for Flight Number: {flight.FlightNumber}");
            }
        }
        Console.WriteLine($"Total flight assigned: {processedFlights}");
        double percentageProcessed = (double)processedFlights / terminal.Flights.Count() * 100;
        Console.WriteLine($"Percentage of Flights Processed Automatically {percentageProcessed:F2}%");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
/* Advanced feature b (ARJUN VIVEKANANTHAN) */
void DisplayAirlineTotalFee()
{
    try
    {
        double SQtotal = 0;
        double MHtotal = 0;
        double JLtotal = 0;
        double CXtotal = 0;
        double QFtotal = 0;
        double TRtotal = 0;
        double EKtotal = 0;
        double BAtotal = 0;
        double SQdiscount = 0;
        double MHdiscount = 0;
        double JLdiscount = 0;
        double CXdiscount = 0;
        double QFdiscount = 0;
        double TRdiscount = 0;
        double EKdiscount = 0;
        double BAdiscount = 0;
        int SQFlightCount = 0;
        int MHFlightCount = 0;
        int JLFlightCount = 0;
        int CXFlightCount = 0;
        int QFFlightCount = 0;
        int TRFlightCount = 0;
        int EKFlightCount = 0;
        int BAFlightCount = 0;
        foreach (var flight in terminal.Flights.Values)
        {
            if (flight.AssignedBoardingGate == null)
            {
                Console.WriteLine($"Flight {flight.FlightNumber} has not been assigned a Boarding Gate. Please assign one!");
                continue;
            }
            else
            {
                continue;
            }
        }
        foreach (var flight in terminal.Flights.Values)
        {
            if (flight.Origin == "Singapore (SIN)")
            {
                if (flight.FlightNumber.Split(" ")[0] == "SQ")
                {
                    SQtotal += 800;
                    SQFlightCount++;
                }
                else if (flight.FlightNumber.Split(" ")[0] == "MH")
                {
                    MHtotal += 800;
                    MHFlightCount++;
                }
                else if (flight.FlightNumber.Split(" ")[0] == "JL")
                {
                    JLtotal += 800;
                    JLFlightCount++;
                }
                else if (flight.FlightNumber.Split(" ")[0] == "CX")
                {
                    CXtotal += 800;
                    CXFlightCount++;
                }
                else if (flight.FlightNumber.Split(" ")[0] == "QF")
                {
                    QFtotal += 800;
                    QFFlightCount++;
                }
                else if (flight.FlightNumber.Split(" ")[0] == "TR")
                {
                    TRtotal += 800;
                    TRFlightCount++;
                }
                else if (flight.FlightNumber.Split(" ")[0] == "EK")
                {
                    EKtotal += 800;
                    EKFlightCount++;
                }
                else if (flight.FlightNumber.Split(" ")[0] == "BA")
                {
                    BAtotal += 800;
                    BAFlightCount++;
                }
            }
            if (flight.Destination == "Singapore (SIN)")
            {
                if (flight.FlightNumber.Split(" ")[0] == "SQ")
                {
                    SQtotal += 800;
                    SQFlightCount++;
                }
                else if (flight.FlightNumber.Split(" ")[0] == "MH")
                {
                    MHtotal += 800;
                    MHFlightCount++;
                }
                else if (flight.FlightNumber.Split(" ")[0] == "JL")
                {
                    JLtotal += 800;
                    JLFlightCount++;
                }
                else if (flight.FlightNumber.Split(" ")[0] == "CX")
                {
                    CXtotal += 800;
                    CXFlightCount++;
                }
                else if (flight.FlightNumber.Split(" ")[0] == "QF")
                {
                    QFtotal += 800;
                    QFFlightCount++;
                }
                else if (flight.FlightNumber.Split(" ")[0] == "TR")
                {
                    TRtotal += 800;
                    TRFlightCount++;
                }
                else if (flight.FlightNumber.Split(" ")[0] == "EK")
                {
                    EKtotal += 800;
                    EKFlightCount++;
                }
                else if (flight.FlightNumber.Split(" ")[0] == "BA")
                {
                    BAtotal += 800;
                    BAFlightCount++;
                }
            }
            if (flight.specialRequestCode != null)
            {
                if (flight.specialRequestCode == "LWTT")
                {
                    if (flight.FlightNumber.Split(" ")[0] == "SQ")
                    {
                        SQtotal += 500;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "MH")
                    {
                        MHtotal += 500;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "JL")
                    {
                        JLtotal += 500;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "CX")
                    {
                        CXtotal += 500;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "QF")
                    {
                        QFtotal += 500;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "TR")
                    {
                        TRtotal += 500;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "EK")
                    {
                        EKtotal += 500;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "BA")
                    {
                        BAtotal += 500;

                    }
                }
                else if (flight.specialRequestCode == "DDJB")
                {
                    if (flight.FlightNumber.Split(" ")[0] == "SQ")
                    {
                        SQtotal += 300;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "MH")
                    {
                        MHtotal += 300;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "JL")
                    {
                        JLtotal += 300;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "CX")
                    {
                        CXtotal += 300;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "QF")
                    {
                        QFtotal += 300;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "TR")
                    {
                        TRtotal += 300;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "EK")
                    {
                        EKtotal += 300;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "BA")
                    {
                        BAtotal += 300;

                    }
                }
                else if (flight.specialRequestCode == "CFFT")
                {
                    if (flight.FlightNumber.Split(" ")[0] == "SQ")
                    {
                        SQtotal += 150;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "MH")
                    {
                        MHtotal += 150;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "JL")
                    {
                        JLtotal += 150;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "CX")
                    {
                        CXtotal += 150;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "QF")
                    {
                        QFtotal += 150;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "TR")
                    {
                        TRtotal += 150;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "EK")
                    {
                        EKtotal += 150;

                    }
                    else if (flight.FlightNumber.Split(" ")[0] == "BA")
                    {
                        BAtotal += 150;

                    }
                }
            }
            bool sqhasexecuted = false;
            bool mhhasexecuted = false;
            bool jlhasexecuted = false;
            bool cxhasexecuted = false;
            bool qfhasexecuted = false;
            bool trhasexecuted = false;
            bool ekhasexecuted = false;
            bool bahasexecuted = false;
            if (SQFlightCount == 5)
            {
                if (sqhasexecuted = false)
                {
                    SQdiscount += (SQtotal / 100 * 3);
                    sqhasexecuted = true;
                }

            }
            else if (MHFlightCount == 5)
            {
                if (mhhasexecuted = false)
                {
                    MHdiscount += (MHtotal / 100 * 3);
                    mhhasexecuted = true;
                }

            }
            else if (JLFlightCount == 5)
            {
                if (jlhasexecuted = false)
                {
                    JLdiscount += (JLtotal / 100 * 3);
                    jlhasexecuted = true;
                }

            }
            else if (CXFlightCount == 5)
            {
                if (cxhasexecuted = false)
                {
                    CXdiscount += (CXtotal / 100 * 3);
                    cxhasexecuted = true;
                }

            }
            else if (QFFlightCount == 5)
            {
                if (qfhasexecuted = false)
                {
                    QFdiscount += (QFtotal / 100 * 3);
                    qfhasexecuted = true;
                }

            }
            else if (TRFlightCount == 5)
            {
                if (trhasexecuted = false)
                {
                    TRdiscount += (TRtotal / 100 * 3);
                    trhasexecuted = true;
                }

            }
            else if (EKFlightCount == 5)
            {
                if (ekhasexecuted = false)
                {
                    EKdiscount += (EKtotal / 100 * 3);
                    ekhasexecuted = true;
                }

            }
            else if (BAFlightCount == 5)
            {
                if (bahasexecuted = false)
                {
                    BAdiscount += (BAtotal / 100 * 3);
                    bahasexecuted = true;
                }

            };
        }
        SQtotal -= SQFlightCount % 3 * 350;
        MHtotal -= MHFlightCount % 3 * 350;
        JLtotal -= JLFlightCount % 3 * 350;
        CXtotal -= CXFlightCount % 3 * 350;
        QFtotal -= QFFlightCount % 3 * 350;
        TRtotal -= TRFlightCount % 3 * 350;
        EKtotal -= EKFlightCount % 3 * 350;
        BAtotal -= BAFlightCount % 3 * 350;
        foreach (var flight in terminal.Flights.Values)
        {
            string timetoconvert = Convert.ToString(flight.ExpectedTime).Split(':')[0];
            int timetocompare = Convert.ToInt32(timetoconvert);
            if (timetocompare < 11 || timetocompare > 9)
            {
                if (flight.FlightNumber.Split(" ")[0] == "SQ")
                {
                    SQdiscount += 110;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "MH")
                {
                    MHdiscount += 110;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "JL")
                {
                    JLdiscount += 110;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "CX")
                {
                    CXdiscount += 110;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "QF")
                {
                    QFdiscount += 110;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "TR")
                {
                    TRdiscount += 110;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "EK")
                {
                    EKdiscount += 110;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "BA")
                {
                    BAdiscount += 110;

                }
            }
            if (flight.Origin == "Bangkok (BKK)" || flight.Origin == "Dubai (DXB)" || flight.Origin == "Tokyo (NRT)")
            {
                if (flight.FlightNumber.Split(" ")[0] == "SQ")
                {
                    SQdiscount += 25;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "MH")
                {
                    MHdiscount += 25;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "JL")
                {
                    JLdiscount += 25;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "CX")
                {
                    CXdiscount += 25;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "QF")
                {
                    QFdiscount += 25;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "TR")
                {
                    TRdiscount += 25;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "EK")
                {
                    EKdiscount += 25;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "BA")
                {
                    BAdiscount += 25;

                }
            }
            if (flight.specialRequestCode == null)
            {
                if (flight.FlightNumber.Split(" ")[0] == "SQ")
                {
                    SQdiscount += 50;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "MH")
                {
                    MHdiscount += 50;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "JL")
                {
                    JLdiscount += 50;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "CX")
                {
                    CXdiscount += 50;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "QF")
                {
                    QFdiscount += 50;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "TR")
                {
                    TRdiscount += 50;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "EK")
                {
                    EKdiscount += 50;

                }
                else if (flight.FlightNumber.Split(" ")[0] == "BA")
                {
                    BAdiscount += 50;

                }
            }
        }
        double sqfinal = SQtotal - SQdiscount;
        double mhfinal = MHtotal - MHdiscount;
        double jlfinal = JLtotal - JLdiscount;
        double cxfinal = CXtotal - CXdiscount;
        double qffinal = QFtotal - QFdiscount;
        double trfinal = TRtotal - TRdiscount;
        double ekfinal = EKtotal - EKdiscount;
        double bafinal = BAtotal - BAdiscount;
        double subfee = SQtotal + MHtotal + JLtotal + CXtotal + QFtotal + TRtotal + EKtotal + BAtotal;
        double subdiscount = SQdiscount + MHdiscount + JLdiscount + CXdiscount + QFdiscount + TRdiscount + EKdiscount + BAdiscount;
        double subfinal = subfee - subdiscount;

        Console.WriteLine("         Airline Name                    Fees Charged         Discount            Final Amount");
        Console.WriteLine($"        Singapore Airlines               ${SQtotal}          ${SQdiscount}       ${sqfinal}");
        Console.WriteLine($"        Malaysia Airlines                ${MHtotal}          ${MHdiscount}       ${mhfinal}");
        Console.WriteLine($"        Japan Airlines                   ${JLtotal}          ${JLdiscount}       ${jlfinal}");
        Console.WriteLine($"        Cathay Pacific                   ${CXtotal}          ${CXdiscount}       ${cxfinal}");
        Console.WriteLine($"        Qantas Airways                   ${QFtotal}          ${QFdiscount}       ${qffinal}");
        Console.WriteLine($"        AirAsia                          ${TRtotal}          ${TRdiscount}       ${trfinal}");
        Console.WriteLine($"        Emirates                         ${EKtotal}          ${EKdiscount}       ${ekfinal}");
        Console.WriteLine($"        British Airways                  ${BAtotal}          ${BAdiscount}       ${bafinal}");
        Console.WriteLine("===============================================================================================");
        Console.WriteLine("                                            Statistics                                                 \n");
        Console.WriteLine("         Airline Fee Subtotal            Airline Discount Subtotal             Final Fee Subtotal");
        Console.WriteLine($"         ${subfee}                       ${subdiscount}                          ${subfinal}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
while (true)
{
    try
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
        else if (option == "8")
        {
            BulkAssignBoardingGates();
        }
        else if (option == "9")
        {
            DisplayAirlineTotalFee();
        }
        else
        {
            Console.WriteLine("Invalid Option!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}