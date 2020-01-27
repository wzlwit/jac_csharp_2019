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
using System.Windows.Shapes;

namespace Day14PeopleDB
{
    /// <summary>
    /// Interaction logic for AddEditDialog.xaml
    /// </summary>
    public partial class AddEditDialog : Window
    {
        Person EditedPerson;

        public AddEditDialog(Window owner, Person editedPerson = null)
        {
            InitializeComponent();
            Owner = owner;
            EditedPerson = editedPerson;
            if (EditedPerson != null)
            {
                btAddEdit.Content = "Update";
                lblId.Content = EditedPerson.Id;
                tbName.Text = EditedPerson.Name;
                tbAge.Text = EditedPerson.Age + "";
            }
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            string ageStr = tbAge.Text;
            // FIXME: handle parsing error
            int age = int.Parse(ageStr);
            if (EditedPerson == null)
            {
                Person person = new Person() { Name = name, Age = age };
                // FIXME: handle SqlException
                Globals.Db.AddPerson(person);
            } else
            {
                EditedPerson.Name = name;
                EditedPerson.Age = age;
                // FIXME: handle SqlException
                Globals.Db.UpdatePerson(EditedPerson);
            }
            DialogResult = true;
        }
    }
}
