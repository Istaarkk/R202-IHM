using Microsoft.Win32;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;

namespace BuggedFileReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog ofd = new OpenFileDialog();
        private string lastOpenFile = "";
        private int count;

        public MainWindow()
        {
            InitializeComponent();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
        }

        /**
         * Exceptions are not handled
         */
        private void BTNOpenFile_Click(object sender, RoutedEventArgs e)
        {
       
                lastOpenFile = TBXFileName.Text;
                loadFile();
            
                     
            
        }

        /**
         * This method should open a Dialog window
         */
        private void BTNOpenDialog_Click(object sender, RoutedEventArgs e)
        {
            bool? result = ofd.ShowDialog();
            if (result!=null && result == true)
            {
                lastOpenFile = ofd.FileName;
                loadFile();    
            }
        }
        private int wordCount()
        {
            count = 0;
            foreach(string line in TBKContent.Text.Split("\n"))
            {
                foreach(string word in line.Split(' ')){
                 
                    foreach (string wordd in line.Split("\'"))
                    {
                        ++count;
                    }
                
                }
            }
            return count;
        }

        /**
         * This method should open the file... Where does the class "File" come from?
         */
        private void loadFile()
        {
            try
            {
                string text = File.ReadAllText(lastOpenFile);
                TBKContent.Text = text;
                LBLWordCountValue.Content = wordCount();
            }
            catch (System.IO.FileNotFoundException ex) {
                MessageBox.Show("File path is incorrect.\nCheck your file path, or check that it is readable.");
            }
            catch (System.ArgumentException ex)
            {
                MessageBox.Show("incorrect input ");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        /**
         * This method must be triggered by the combobox. Did we configure all the possible options?
         */
        private void CBXCase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            if(cbx.SelectedIndex == 0 && lastOpenFile != "")
            {
                loadFile();
            }
            if(cbx.SelectedIndex == 0)
            {
                TBKContent.Text = TBKContent.Text.ToUpper();
            }
            if (cbx.SelectedIndex == 1)
            {
                TBKContent.Text= TBKContent.Text.ToLower();
            }
        }
    }
}
