using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day05AirportTravel
{
    class InvalidDataException : Exception
    {
        public InvalidDataException() { }
        public InvalidDataException(string msg) : base(msg) { }
        public InvalidDataException(string msg, Exception inner) : base(msg, inner) { }
    }

    class Airport
    {
        public string City;
        public double Latitude, Longitude;

        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                if (!Regex.Match(value, "^[A-Z]{3}$").Success)
                {
                    throw new InvalidDataException("Airport code must have exactly 3 upper case letters");
                }
                _code = value;
            }
        }
    }

    class AirportNotFoundException : Exception { }

    class Program
    {
        static List<Airport> airportList = new List<Airport>();


        static Airport FindAirportByCodeException(string code)
        {
            foreach (Airport a in airportList)
            {
                if (a.Code == code)
                {
                    return a;
                }
            }
            throw new AirportNotFoundException();
        }


        static void FindDistanceBetweenTwoAirportsByCodesException()
        {
            try
            {
                Console.Write("Enter first airport code: ");
                string code1 = Console.ReadLine();
                Airport airport1 = FindAirportByCode(code1);

                Console.Write("Enter second airport code: ");
                string code2 = Console.ReadLine();
                Airport airport2 = FindAirportByCode(code2);

                var sCoord = new GeoCoordinate(airport1.Latitude, airport1.Longitude);
                var eCoord = new GeoCoordinate(airport2.Latitude, airport2.Longitude);
                double distanceKm = sCoord.GetDistanceTo(eCoord) / 1000;
                Console.WriteLine("Distance between {0} and {1} is {3:0.000}km", code1, code2, distanceKm);
            } catch (AirportNotFoundException ex)
            {
                Console.WriteLine("One or both airport codes you entered were not found.");
            }
        }

        static Airport FindAirportByCode(string code)
        {
            foreach (Airport a in airportList)
            {
                if (a.Code == code)
                {
                    return a;
                }
            }
            return null;
        }

        static void FindDistanceBetweenTwoAirportsByCodes()
        {
            Console.Write("Enter first airport code: ");
            string code1 = Console.ReadLine();
            Console.Write("Enter second airport code: ");
            string code2 = Console.ReadLine();
            Airport airport1 = FindAirportByCode(code1);
            Airport airport2 = FindAirportByCode(code2);
            if (airport1 == null || airport2 == null)
            {
                Console.WriteLine("One or both airport codes you entered were not found.");
                return;
            }
            var coord1 = new GeoCoordinate(airport1.Latitude, airport1.Longitude);
            var coord2 = new GeoCoordinate(airport2.Latitude, airport2.Longitude);
            double distanceKm = coord1.GetDistanceTo(coord2) / 1000;
            Console.WriteLine("Distance between {0} and {1} is {3:0.000}km", code1, code2, distanceKm); 
        }


        static void FindNearestAirportToCode()
        {
            if (airportList.Count < 2)
            {
                Console.WriteLine("You need at least 2 airports in your database to find the nearest.");
                return;
            }
            //
            Console.Write("Enter airport code: ");
            string code = Console.ReadLine();
            Airport airport = FindAirportByCode(code);
            if (airport == null)
            {
                Console.WriteLine("Airport not found");
                return;
            }
            //
            double distanceKm = double.MaxValue;
            Airport nearest = null;
            foreach (Airport a in airportList)
            {
                if (a == airport) continue;
                var coord1 = new GeoCoordinate(airport.Latitude, airport.Longitude);
                var coord2 = new GeoCoordinate(a.Latitude, a.Longitude);
                double newDistanceKm = coord1.GetDistanceTo(coord2) / 1000;
                if (newDistanceKm < distanceKm)
                {// found new closest airport
                    nearest = a;
                    distanceKm = newDistanceKm;
                }
            }
            Console.WriteLine("Aiport nearest to {0} is {1} which is {3:0.000}km away",
                code, nearest.Code, distanceKm);
        }


        static void Main(string[] args)
        {
            switch ()
            {
                case 2:
                    FindDistanceBetweenTwoAirportsByCodes();
                    break;
            }

        }
    }
}
