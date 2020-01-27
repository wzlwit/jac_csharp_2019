using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Day14PeopleDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Person> PeopleList = new List<Person>();

        public MainWindow()
        {
            InitializeComponent();
            lvPeople.ItemsSource = PeopleList;

            // 1 - connect to datase
            try
            {
                Globals.Db = new Database();
                // 2 - load data in ListView
                ReloadList();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Fatal error: unable to connect to database\n" + ex.Message,
                    "People Database", MessageBoxButton.OK, MessageBoxImage.Error);
                Close(); // close the main window, terminate the program
            }
        }

        private void ReloadList()
        {
            try
            {
                List<Person> list = Globals.Db.GetAllPeople();
                PeopleList.Clear();
                foreach (Person p in list)
                {
                    PeopleList.Add(p);
                }
                lvPeople.Items.Refresh();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error executing SQL query:\n" + ex.Message,
                    "People Database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FileExit_MenuClick(object sender, RoutedEventArgs e)
        {

        }

        private void AddPerson_MenuClick(object sender, RoutedEventArgs e)
        {
            AddEditDialog addEditDialog = new AddEditDialog(this);
            if (addEditDialog.ShowDialog() == true)
            {
                ReloadList();
            }
        }

        private void DeleteItem_ContexMenuClick(object sender, RoutedEventArgs e)
        {
            Person currPerson = lvPeople.SelectedItem as Person;
            if (currPerson == null) return;
            //
            MessageBoxResult result = MessageBox.Show(this, "Are you sure you want to delete:\n" +
                currPerson, "Confirm delete", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            //
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    Globals.Db.DeletePerson(currPerson.Id);
                    ReloadList();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error executing SQL query:\n" + ex.Message,
                        "People Database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditItem_ContexMenuClick(object sender, RoutedEventArgs e)
        {
            LvPeople_MouseDoubleClick(sender, null); // "redirect" the event
        }

        private void LvPeople_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Person currPerson = lvPeople.SelectedItem as Person;
            if (currPerson == null) return;

            AddEditDialog addEditDialog = new AddEditDialog(this, currPerson);
            if (addEditDialog.ShowDialog() == true)
            {
                ReloadList();
            }
        }
    }
}
