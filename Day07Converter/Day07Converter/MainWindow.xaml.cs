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

namespace Day07Converter
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

        private void ButtonConvert_Click(object sender, RoutedEventArgs e)
        {
            string celStr = tbCelsius.Text;
            double cel;
            if (!double.TryParse(celStr, out cel)) {
                MessageBox.Show("Invalid input: value must be numerical");
                return;
            }
            double fah = cel * 9 / 5 + 32;
            lblResult.Content = String.Format("{0:0.##} Fahrenheit", fah);
        }
    }
}
