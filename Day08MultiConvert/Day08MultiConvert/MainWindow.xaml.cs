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

namespace Day08MultiConvert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TbInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            Recalculate();
        }

        private void RbAny_Click(object sender, RoutedEventArgs e)
        {
            Recalculate();
        }

        private void Recalculate()
        {
            // STEP 1: read input, validate, etc.
            string inputStr = tbInput.Text;
            if (inputStr == "")
            {
                return;
            }
            double inputVal;
            if (!double.TryParse(inputStr, out inputVal))
            {
                MessageBox.Show("Invalid input, must be numerical");
                tbInput.Text = "";
                return;
            }
            // STEP 2: convert from Input to Celsius
            double celsius = 0;
            if (rbInputCel.IsChecked == true)
            {
                celsius = inputVal;
            }
            else if (rbInputFah.IsChecked == true)
            {
                celsius = (inputVal - 32) * 5 / 9;
            }
            else if (rbInputKel.IsChecked == true)
            {
                celsius = inputVal - 273.15;
            }
            else
            {
                MessageBox.Show("Internal error. Unknown input scale.");
                return;
            }
            // STEP 3: convert from Celsius to Ouput and display result
            double result = 0;
            string suffix = "";
            if (rbOutCel.IsChecked == true)
            {
                result = celsius;
                suffix = "C";
            }
            else if (rbOutFah.IsChecked == true)
            {
                result = celsius * 9 / 5 + 32;
                suffix = "F";
            }
            else if (rbOutKel.IsChecked == true)
            {
                result = celsius + 273.15;
                suffix = "K";
            }
            tbOutput.Text = String.Format("{0:0.##} {1}", result, suffix);
        }

    }
}
