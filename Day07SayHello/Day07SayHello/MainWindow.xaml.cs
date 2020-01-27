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

namespace Day07SayHello
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

        private bool VerifyInputs()
        {
            string name = tbName.Text;
            if (!Regex.IsMatch(name, @"^[^;]{2,30}$"))
            {
                MessageBox.Show("Name invalid, must be 2-30 characters, no semicolons");
                return false;
            }
            string ageStr = tbAge.Text;
            int age; 
            if ((!int.TryParse(ageStr, out age)) || age < 1 || age > 150)
            {
                MessageBox.Show("Age invalid, must be an integer between 1-150");
                return false;
            }
            return true;
        }

        private void ButtonSayHello_Click(object sender, RoutedEventArgs e)
        {
            if (!VerifyInputs()) return;
            string name = tbName.Text;
            int age = int.Parse(tbAge.Text);
            MessageBox.Show(String.Format("Hello {0} you are {1} y/o. Nice to meet you",
                name, age));
        }

        private void ButtonSaveAndClear_Click(object sender, RoutedEventArgs e)
        {
            if (!VerifyInputs()) return;
            string name = tbName.Text;
            int age = int.Parse(tbAge.Text);
            File.AppendAllText(@"people.txt", String.Format("{0};{1}{2}", name, age,
                Environment.NewLine));
            MessageBox.Show("Data saved");
            tbName.Text = "";
            tbAge.Text = "";
        }
    }
}

