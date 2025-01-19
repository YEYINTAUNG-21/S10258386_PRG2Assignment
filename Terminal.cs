﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace S10268975C_PRG2Assignment
{
    public class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();

        public Terminal() { }
        public Terminal(string tn, Dictionary<string, Airline> a, Dictionary<string, Flight> f, Dictionary<string, BoardingGate> bg, Dictionary<string, double> gf)
        {
            TerminalName = tn;
            Airlines = a;
            Flights = f;
            BoardingGates = bg;
            GateFees = gf;
        }

        public bool AddAirline(Airline airline)
        {
            if (!Airlines.ContainsKey(airline.Code))
            {
                Airlines[airline.Code] = airline;
                return true;
            }
            return false;
        }
        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (!BoardingGates.ContainsKey(boardingGate.GateName))
            {
                BoardingGates[boardingGate.GateName] = boardingGate;
            }
        }
        public Airline GetAirlineFromFlight(Flight flight)
        {
            foreach (var airline in Airlines.Values)
            {
                if (airline.Flights.ContainsValue(flight))
                {
                    return airline;
                }
            }
            return null;
        }
        public void PrintAirlineFees()
        {
            foreach (var airline in Airlines.Values)
            {
                Console.WriteLine($"Airline: {airline.Name}, Fees: {airline.CalcualteFees()}");
            }
        }
        public override string ToString()
        {
            return $"Terminal Name: {TerminalName}, Airlines: {Airlines.Count}, Flights: {Flights.Count}, Boarding Gates: {BoardingGates.Count}, Gate Fees: {GateFees.Count}"
        }
    }
}
