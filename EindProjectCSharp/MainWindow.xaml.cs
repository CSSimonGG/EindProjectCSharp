

using EindProjectCSharp.Classes;
using System.Data;
using System.Windows;

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
