using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Day13AirportsCRUD
{
    public class Airport
    {
        public Airport() { }
        public Airport(string code, string city, double latitude, double longitude, int elevationMeters)
        {
            Code = code;
            City = city;
            Latitude = latitude;
            Longitude = longitude;
            ElevationMeters = elevationMeters;
        }
        public Airport(string dataLine)
        { // deserialize from a line of text

        }
        public string toDataString()
        { // serialize to line of text

        }

        public string Code; // exactly 3 upper case letters
        public string City; // 1-100 characters, excluding ; (semicolons)
        public double Latitude, Longitude; // check wikipedia
        public int ElevationMeters; // -1000 to 10000 using a slider 50m increments
        public override string ToString() {
            return string.Format("{0} in {1} at {2}lat/{3}lng {4}m high",
                Code, City, Latitude, Longitude, ElevationMeters);
        }
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Airport> airportList = new List<Airport>();

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
