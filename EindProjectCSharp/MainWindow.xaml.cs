

using EindProjectCSharp.Classes;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace EindProjectCSharp
{
    // Simon de Klerk
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GamesDB _gamesDB = new GamesDB(); // Get games database
        public MainWindow()
        {
            InitializeComponent();
            FillDataGrid(); // Display games
        }
        private void FillDataGrid()
        {
            DataTable students = _gamesDB.SelectGames();
            if (students != null)
            {
                // gridGames
            }
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            // Open profile window
            Profile profile = new Profile();
            profile.ShowDialog();

            FillDataGrid(); // Update displayed games 
        }
    }
}
