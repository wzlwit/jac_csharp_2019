using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Day12TodosInFile
{

    public class TodoItem
    {
        // Factory Pattern - not on quiz 1
        public static TodoItem CreateTodoFromDataLine(string dataLine)
        {
            TodoItem item = new TodoItem();
            string[] data = dataLine.Split(';');
            if (data.Length != 3)
            {
                throw new InvalidDataException("Data line must be composed of 3 items exactly");
            }
            // TODO: I may want to add task length verification
            item.Task = data[0];
            try
            {
                item.DueDate = DateTime.ParseExact(data[1], "yyyy-MM-dd", null);
                // TODO: check that values are either done or pending, and nothing else
                item.IsDone = data[2] == "done";
            }
            catch (FormatException ex)
            {
                // exception chaining - translating one exception into another
                throw new InvalidDataException("Data format invalid", ex);
            }
            return item;
        }


        public TodoItem(string task, DateTime dueDate, bool isDone)
        {
            Task = task;
            DueDate = dueDate;
            IsDone = isDone;
        }

        public TodoItem(string dataLine)
        { // de-serialization
            string[] data = dataLine.Split(';');
            if (data.Length != 3)
            {
                throw new InvalidDataException("Data line must be composed of 3 items exactly");
            }
            // TODO: I may want to add task length verification
            Task = data[0];
            try
            {
                DueDate = DateTime.ParseExact(data[1], "yyyy-MM-dd", null);
                // TODO: check that values are either done or pending, and nothing else
                IsDone = data[2] == "done";
            }
            catch (FormatException ex)
            {
                // exception chaining - translating one exception into another
                throw new InvalidDataException("Data format invalid", ex);
            }
        }
        public TodoItem() { }
        public string Task;
        public DateTime DueDate;
        public bool IsDone;
        public override string ToString()
        {
            return string.Format("{0} by {1} is {2}",
                Task, DueDate.ToShortDateString(), IsDone ? "done" : "pending");
        }
        public string ToDataLine()
        { // serialization
            return string.Format("{0};{1};{2}", Task, DueDate.ToString("yyyy-MM-dd"), IsDone ? "done" : "pending");
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string FILE_NAME = @"..\..\..\todolist.dat";

        List<TodoItem> todoItemsList = new List<TodoItem>();

        public MainWindow()
        {
            /* // fake data
            todoItemsList.Add(new TodoItem() { Task = "buy milk", DueDate = DateTime.ParseExact("2019-10-27", "yyyy-MM-dd", null) , IsDone = false });
            todoItemsList.Add(new TodoItem() { Task = "feed the cat", DueDate = new DateTime(), IsDone = true });
            todoItemsList.Add(new TodoItem() { Task = "buy cat food", DueDate = new DateTime(), IsDone = false }); */
            //
            InitializeComponent();
            LoadDataFromFile(FILE_NAME);
            lvTodos.ItemsSource = todoItemsList;
            dpDueDate.SelectedDate = DateTime.Today;
        }

        private void FileExportSelected_MenuClick(object sender, RoutedEventArgs e)
        {
            if (lvTodos.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select one or more items for export first", "Todo List", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Todo Lists|*.todos|All files|*.*";
            dialog.Title = "Export todo items";
            dialog.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (dialog.FileName != "")
            {
                try
                {
                    List<TodoItem> selectedItemsList = new List<TodoItem>();
                    foreach (TodoItem item in lvTodos.SelectedItems)
                    {
                        selectedItemsList.Add(item);
                    }
                    SaveDataToFile(dialog.FileName, selectedItemsList);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error saving to file: " + ex.Message, "Todo List", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FileImport_MenuClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Todo Lists|*.todos|All files|*.*";
            dialog.Title = "Import todo items";
            dialog.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (dialog.FileName != "")
            {
                try
                {
                    LoadDataFromFile(dialog.FileName);
                    lvTodos.Items.Refresh();
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error loading data from file: " + ex.Message, "Todo List", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FileExit_MenuClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAddUpdateTask_Click(object sender, RoutedEventArgs e)
        {
            string task = tbTask.Text;
            if (dpDueDate.SelectedDate == null)
            {
                MessageBox.Show("You must select a due date for the task", "Todo List", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateTime dueDate = dpDueDate.SelectedDate.Value;
            bool isDone = rbDone.IsChecked == true;
            if (task.Length < 1 || task.Length > 100 || task.Contains(";"))
            { // or use regexp: @"^[^;]{1,100}$"
                MessageBox.Show("Task description must be 1-100 characters long, no semicolons", "Todo List", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Button senderButton = sender as Button;
            if (senderButton.Name == "btUpdateTask")
            {
                TodoItem todo = lvTodos.SelectedItem as TodoItem;
                todo.Task = task;
                todo.DueDate = dueDate;
                todo.IsDone = isDone;
            }
            else
            {
//                TodoItem todo = new TodoItem() { Task = task, DueDate = dueDate, IsDone = isDone };
                TodoItem todo = new TodoItem(task, dueDate, isDone);
                todoItemsList.Add(todo);
                // cleanup
                tbTask.Text = "";
                dpDueDate.SelectedDate = DateTime.Today;
                rbPending.IsChecked = true;
            }
            lvTodos.Items.Refresh();
        }

        public void SaveDataToFile(string fileName, List<TodoItem> itemsList)
        {
            List<string> linesList = new List<string>();
            try
            {
                foreach (TodoItem todo in itemsList)
                {
                    linesList.Add(todo.ToDataLine());
                }
                File.WriteAllLines(fileName, linesList);
                sbLastOp.Text = string.Format("Wrote {0} todo items to file {1}", itemsList.Count, fileName);
            } catch (IOException ex)
            {
                MessageBox.Show("Error saving to file: " + ex.Message, "Todo List", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void LoadDataFromFile(string fileName)
        {
            try
            {
                string [] linesList = File.ReadAllLines(fileName);
                foreach (string line in linesList)
                {
                    try
                    {
                        todoItemsList.Add(new TodoItem(line));
                    }
                    catch (InvalidDataException ex)
                    {
                        // FIXME: Collect errors and only show a single error message box after the loop
                        MessageBox.Show("Error reading line from file: " + ex.Message, "Todo List", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (FileNotFoundException ex) { } // ignore
            catch (IOException ex)
            {
                MessageBox.Show("Error saving to file: " + ex.Message, "Todo List", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveDataToFile(FILE_NAME, todoItemsList);
        }

        private void SortByTask_MenuClick(object sender, RoutedEventArgs e)
        {
            // Using Lambda Notation
            todoItemsList = todoItemsList.OrderBy(todoItem => todoItem.Task).ToList<TodoItem>();
            lvTodos.ItemsSource = todoItemsList;
        }

        private void SortByDueDate_MenuClick(object sender, RoutedEventArgs e)
        {
            // Using query syntax
            todoItemsList = (from todo in todoItemsList orderby todo.DueDate select todo).ToList<TodoItem>();
            lvTodos.ItemsSource = todoItemsList;
        }

        private void ButtonDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            TodoItem item = lvTodos.SelectedItem as TodoItem;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete:\n" + item.ToString(), "Todo list", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.OK)
            {
                // option 1:
                todoItemsList.Remove(item);
                lvTodos.Items.Refresh();
                // option 2: fails with exception
                // lvTodos.Items.Remove(item);
            }
        }

        private void LvTodos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TodoItem item = lvTodos.SelectedItem as TodoItem;
            if (item == null)
            { // nothing selected
                btUpdateTask.IsEnabled = false;
                btDeleteTask.IsEnabled = false;
                return;
            }
            btUpdateTask.IsEnabled = true;
            btDeleteTask.IsEnabled = true;
            // load data of currently selected item (first selected)
            tbTask.Text = item.Task;
            dpDueDate.SelectedDate = item.DueDate;
            rbDone.IsChecked = item.IsDone;
        }
    }
}
