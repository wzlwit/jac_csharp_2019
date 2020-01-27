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

namespace Day06HelloWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TheButton.Click += Button_Click;
            TheButton.Click += Button_AlternativeClick;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            MessageBox.Show(String.Format("Hello {0}, nice to meet you!", name));
        }

        void Button_AlternativeClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("BUTTON CLICKED !!!");
        }

    }
}
