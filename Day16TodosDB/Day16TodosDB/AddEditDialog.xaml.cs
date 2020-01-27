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

namespace Day16TodosDB
{
    /// <summary>
    /// Interaction logic for AddEditDialog.xaml
    /// </summary>
    public partial class AddEditDialog : Window
    {
        Todo EditedTodo;

        public AddEditDialog(Window owner, Todo editedTodo = null)
        {
            InitializeComponent();
            Owner = owner;
            EditedTodo = editedTodo;
            if (EditedTodo != null)
            {
                btAddEdit.Content = "Update";
                lblId.Content = EditedTodo.Id;
                tbTask.Text = EditedTodo.Task;
                dpDueDate.SelectedDate = EditedTodo.DueDate;
                cbIsDone.IsChecked = (EditedTodo.IsDone == ETaskStatus.Done);
            }
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {
            string task = tbTask.Text;
            DateTime dueDate = dpDueDate.SelectedDate.Value;
            ETaskStatus isDone = (cbIsDone.IsChecked == true ? ETaskStatus.Done : ETaskStatus.Pending);
            if (EditedTodo == null)
            {
                Todo Todo = new Todo() { Task = task, DueDate = dueDate, IsDone = isDone };
                // FIXME: handle SqlException
                Globals.Db.AddTodo(Todo);
            }
            else
            {
                EditedTodo.Task = task;
                EditedTodo.DueDate = dueDate;
                EditedTodo.IsDone = isDone;
                // FIXME: handle SqlException
                Globals.Db.UpdateTodo(EditedTodo);
            }
            DialogResult = true;
        }
    }
}
