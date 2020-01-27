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

namespace Day10ScoopSelector
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

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem item = lvFlavours.SelectedItem as ListViewItem;
            if (item == null)
            {
                MessageBox.Show("Select a flavour first!", "Scoop Selector", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            // Option 1: add string directly to the list
            //lvOrdered.Items.Add(item.Content);
            // Option 2: with ListViewItem
            ListViewItem dupItem = new ListViewItem();
            dupItem.Content = item.Content;
            lvOrdered.Items.Add(dupItem); // */
        }

        private void ButtonDeleteScoop_Click(object sender, RoutedEventArgs e)
        {
            // string item = lvOrdered.SelectedItem as string;
            ListViewItem item = lvOrdered.SelectedItem as ListViewItem;
            if (item == null)
            {
                MessageBox.Show("Select a flavour first!", "Scoop Selector", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            lvOrdered.Items.RemoveAt(lvOrdered.SelectedIndex);
        }

        private void ButtonClearAll_Click(object sender, RoutedEventArgs e)
        {
            lvOrdered.Items.Clear();
        }
    }
}
