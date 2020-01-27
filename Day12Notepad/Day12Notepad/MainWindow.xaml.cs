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

namespace Day12Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string CurrentlyOpenFilePath = ""; // no file is open
        bool IsDocumentModified = false;

        public MainWindow()
        {
            InitializeComponent();
            updateStatus();
        }

        private void updateStatus()
        {
            string statusBarText = CurrentlyOpenFilePath == "" ? "No file open" : CurrentlyOpenFilePath;
            string windowTitle = IsDocumentModified ? "(modified)" : "";
            sbLastOp.Text = statusBarText;
            Title = windowTitle;
        }

        private void TbDocument_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsDocumentModified = true;
            updateStatus();
            // TODO: update window title here as well
        }

        private void FileNew_MenuClick(object sender, RoutedEventArgs e)
        {
            if (IsDocumentModified)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save unsaved changes?", "NotepadX", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes: // same as Menu->Save
                        FileSave_MenuClick(sender, e);
                        break;
                    case MessageBoxResult.No:
                        tbDocument.Text = "";
                        IsDocumentModified = false;
                        CurrentlyOpenFilePath = "";
                        updateStatus();
                        break;
                    case MessageBoxResult.Cancel:                        
                        break;
                }
            }
            
        }

        private void FileSave_MenuClick(object sender, RoutedEventArgs e)
        {
            if (CurrentlyOpenFilePath == "")
            {
                FileSaveAs_MenuClick(sender, e);
            }
            else
            {
                File.WriteAllText(CurrentlyOpenFilePath, tbDocument.Text);
                IsDocumentModified = false;
                updateStatus();
            }
        }

        private void FileSaveAs_MenuClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text files|*.txt|All files|*.*";
            dialog.Title = "Save a text file";
            dialog.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (dialog.FileName != "")
            {
                CurrentlyOpenFilePath = dialog.FileName;
                File.WriteAllText(CurrentlyOpenFilePath, tbDocument.Text);
                IsDocumentModified = false;
                updateStatus();
            }
        }
    }
}
