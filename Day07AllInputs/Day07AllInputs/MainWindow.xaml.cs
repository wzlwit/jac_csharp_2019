using System;
using System.Collections.Generic;
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

namespace Day07AllInputs
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

        private void ButtonRegisterMe_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            if (!Regex.IsMatch(name, @"^[^;]{1,}$"))
            {
                MessageBox.Show("Name invalid, must be not empty, no semicolons");
                return;
            }
            string age = "";
            if (rbBelow18.IsChecked == true)
            {
                age = "below 18";
            } else if (rb18to35.IsChecked == true)
            {
                age = "18 to 35";
            } else if (rb36andup.IsChecked == true)
            {
                age = "36 or above";
            } else
            {
                // Internal Error
                MessageBox.Show("Internal error");
                return;
            }
            //
            List<string> animalsList = new List<string>();
            if (cbCat.IsChecked == true)
            {
                animalsList.Add("Cat");
            }
            if (cbDog.IsChecked == true)
            {
                animalsList.Add("Dog");
            }
            if (cbOther.IsChecked == true)
            {
                animalsList.Add("Other");
            }
            string animals = String.Join(",", animalsList);
            //
            string continent = cmbContinent.Text;
            int tempC = (int) slTempC.Value;
            // TODO: write to file
        }
    }
}
