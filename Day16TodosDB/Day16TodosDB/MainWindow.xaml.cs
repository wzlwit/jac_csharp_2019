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

namespace Day16TodosDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum ETaskSortOrder { Id, Task, DueDate, IsDone };

        ETaskSortOrder TaskSortOrder = ETaskSortOrder.Id;

        List<Todo> TodosList = new List<Todo>();

        public MainWindow()
        {
            InitializeComponent();
            lvTodos.ItemsSource = TodosList;

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
                    "Todos Database", MessageBoxButton.OK, MessageBoxImage.Error);
                Close(); // close the main window, terminate the program
            }
        }

        private void ReloadList()
        {
            try
            {
                //List<Todo> list = Globals.Db.GetAllTodos(TaskSortOrder.ToString());
                List<Todo> list = Globals.Db.GetAllTodos();
                // TODO: try and sort using Dynamic Linq
                switch (TaskSortOrder)
                {
                    case ETaskSortOrder.Id:
                        list = list.OrderBy(t => t.Id).ToList<Todo>();
                        break;
                    case ETaskSortOrder.Task:
                        list = list.OrderBy(t => t.Task).ToList<Todo>();
                        break;
                    case ETaskSortOrder.DueDate:
                        list = list.OrderBy(t => t.DueDate).ToList<Todo>();
                        break;
                    case ETaskSortOrder.IsDone:
                        list = list.OrderBy(t => t.IsDone).ToList<Todo>();
                        break;
                    default:
                        MessageBox.Show("Internal error: unknown sort column",
                        "Todos Database", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }

                TodosList.Clear();
                foreach (Todo p in list)
                {
                    TodosList.Add(p);
                }
                lvTodos.Items.Refresh();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error executing SQL query:\n" + ex.Message,
                    "Todos Database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FileExit_MenuClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddTodo_MenuClick(object sender, RoutedEventArgs e)
        {
            AddEditDialog addEditDialog = new AddEditDialog(this);
            if (addEditDialog.ShowDialog() == true)
            {
                ReloadList();
            }
        }

        private void DeleteItem_ContexMenuClick(object sender, RoutedEventArgs e)
        {
            Todo currTodo = lvTodos.SelectedItem as Todo;
            if (currTodo == null) return;
            //
            MessageBoxResult result = MessageBox.Show(this, "Are you sure you want to delete:\n" +
                currTodo, "Confirm delete", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            //
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    Globals.Db.DeleteTodo(currTodo.Id);
                    ReloadList();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error executing SQL query:\n" + ex.Message,
                        "Todos Database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditItem_ContexMenuClick(object sender, RoutedEventArgs e)
        {
            LvTodos_MouseDoubleClick(sender, null); // "redirect" the event
        }

        private void LvTodos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Todo currTodo = lvTodos.SelectedItem as Todo;
            if (currTodo == null) return;

            AddEditDialog addEditDialog = new AddEditDialog(this, currTodo);
            if (addEditDialog.ShowDialog() == true)
            {
                ReloadList();
            }
        }

        private void lvTodos_HeaderClick(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            if (headerClicked == null) { return; }
            switch (headerClicked.Tag)
            {
                case "Id":
                    TaskSortOrder = ETaskSortOrder.Id;
                    break;
                case "Task":
                    TaskSortOrder = ETaskSortOrder.Task;
                    break;
                case "DueDate":
                    TaskSortOrder = ETaskSortOrder.DueDate;
                    break;
                case "Status":
                    TaskSortOrder = ETaskSortOrder.IsDone;
                    break;
                default:
                    MessageBox.Show("Internal error: unknown sort column",
                        "Todos Database", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
            ReloadList();
        }
    }
}
