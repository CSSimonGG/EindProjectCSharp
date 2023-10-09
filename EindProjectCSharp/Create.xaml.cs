using EindProjectCSharp.Classes;
using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace EindProjectCSharp
{
    // Simon de Klerk
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        GamesDB _gamesDB = new GamesDB(); // Get games database

        public Create()
        {
            InitializeComponent();
            FillStudioSelection();
        }

        private void FillStudioSelection()
        {
            // Fill combobox with all studios from database
            cmbStudio.ItemsSource = _gamesDB.SelectStudios();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            // (Try to) Put game into database
            if(!string.IsNullOrWhiteSpace(tbTitle.Text)) // Check for title input
            {
                if (!string.IsNullOrWhiteSpace(tbDescription.Text)) // Check for description input
                {
                    if (!string.IsNullOrWhiteSpace(tbImagePath.Text)) // Check for image path input
                    {
                        if (!string.IsNullOrEmpty(cmbStudio.Text)) // Check for studio input
                        {
                            MessageBox.Show("Game Created");
                            this.Close(); // Close window
                        }
                        else
                        {
                            MessageBox.Show("No Studio Input");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Image Path Input");
                    }
                }
                else
                {
                    MessageBox.Show("No Description Input");
                }
            }
            else
            {
                MessageBox.Show("No Title Input");
            }
        }
    }
}
