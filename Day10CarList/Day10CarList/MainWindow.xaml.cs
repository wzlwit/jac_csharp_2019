using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Day10CarList
{
    public class Car
    {
        public string Make;
        public string CarSize; // FIXME: change into Enum
        public string Features; // FIXME: Dictionary of Enum or string
        public string Plates;
        public double WeightTonnes;
        public override string ToString()
        {
            return String.Format("{0} is {1} with {2} reg {3}, {4}t",
                Make, CarSize, Features, Plates, WeightTonnes);
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Car> carList = new List<Car>();

        bool hasUnsavedData = false;

        public MainWindow()
        {
            InitializeComponent();

            carList.Add(new Car() { Make = "Audi", CarSize = "Compact", Features = "", Plates = "AZL867", WeightTonnes = 1.23 });
            carList.Add(new Car() { Make = "Toyota", CarSize = "SUV", Features = "", Plates = "LKJ76D", WeightTonnes = 1.39 });
            carList.Add(new Car() { Make = "VW", CarSize = "Van", Features = "", Plates = "U89N65", WeightTonnes = 1.63 });

            lvCars.ItemsSource = carList; // give data to ListView

        }

        private void FileExportSelected_MenuClick(object sender, RoutedEventArgs e)
        {
            if (lvCars.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one car before exporting", "Car List", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text files|*.txt|All files|*.*";
            saveFileDialog1.Title = "Save a data file";
            saveFileDialog1.ShowDialog();

            // TODO: save data to file, semicolon separated
            List<string> linesList = new List<string>();
            if (saveFileDialog1.FileName != "")
            {
                // ONLY SAVE SELECTED CARS
                foreach (Car car in lvCars.SelectedItems)
                {
                    linesList.Add(String.Format("{0};{1};{2};{3};{4:0.00}", car.Make, car.CarSize, car.Features, car.Plates, car.WeightTonnes));
                }
                try
                {
                    File.WriteAllLines(saveFileDialog1.FileName, linesList);
                    hasUnsavedData = false;
                } catch (IOException ex)
                {
                    MessageBox.Show("Error saving to file: " + ex.Message, "Car List", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void FileExit_MenuClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAddAndClean_Click(object sender, RoutedEventArgs e)
        {
            string carMake = comboCarMake.Text;
            if (carMake == "")
            {
                MessageBox.Show("Select car make", "Car List", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            //
            string carSize = "";
            if (rbCarSizeCompact.IsChecked == true)
            {
                carSize = "Compact";
            } else if (rbCarSizeVan.IsChecked == true)
            {
                carSize = "Van";
            } else if (rbCarSizeSUV.IsChecked == true)
            {
                carSize = "SUV";
            } else
            {
                MessageBox.Show("Internal error, invalid Car Size", "Car List", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //
            List<string> featuresList = new List<string>();
            if (cbFeaturesABS.IsChecked == true)
            {
                featuresList.Add("ABS");
            }
            if (cbFeaturesBluetooth.IsChecked == true)
            {
                featuresList.Add("Bluetooth");
            }
            if (cbFeaturesOther.IsChecked == true)
            {
                featuresList.Add("Other");
            }
            string features = string.Join(",", featuresList);
            //
            string plates = tbPlates.Text;
            if (!Regex.Match(plates, @"^[0-9A-Z]{3,10}$").Success)
            {
                MessageBox.Show("Car plates must be composed of 3-10 uppercase letters or digits", "Car List", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            double weight = slWeightTonnes.Value;
            weight = Math.Round(weight, 2, MidpointRounding.AwayFromZero);
            // add the car
            Car car = new Car() { Make = carMake, CarSize = carSize, Features = features, Plates = plates, WeightTonnes = weight };
            carList.Add(car);
            lvCars.Items.Refresh();
            hasUnsavedData = true;
            // cleanup GUI inputs
            comboCarMake.Text = "";
            rbCarSizeCompact.IsChecked = true;
            cbFeaturesABS.IsChecked = false;
            cbFeaturesBluetooth.IsChecked = false;
            cbFeaturesOther.IsChecked = false;
            tbPlates.Text = "";
            slWeightTonnes.Value = 0;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // TODO: Warn user of any unsaved data
            // present OK/Cancel dialog is hasUnsavedData is true
            // TODO: also handle window closing by pressing X on window frame
            if (hasUnsavedData)
            {
                MessageBoxResult result = MessageBox.Show("Unsaved data. Are you sure  you want to exit?", "Car list", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }            
        }
    }
}
