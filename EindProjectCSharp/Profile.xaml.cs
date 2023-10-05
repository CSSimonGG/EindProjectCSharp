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
using System.Windows.Shapes;

namespace EindProjectCSharp
{
    // Simon de Klerk
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        GamesDB _gamesDB = new GamesDB(); // Get games database

        public Profile()
        {
            InitializeComponent();
            FillDataGrid();
        }
        private void FillDataGrid()
        {
            // Get all games from database
            DataTable games = _gamesDB.SelectGames();

            if (games != null)
            {
                // Set itemsource of datagrid to the games from the database
                dgGames.ItemsSource = games.DefaultView;
            }
        }


        // Create new game
        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            // Open create window to make new game
            Create create = new Create();
            create.ShowDialog();

            FillDataGrid(); // Update datagrid with games
        }

        // Update game
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = dgGames.SelectedItem as DataRowView; // Get selected row

            // Check if a row has been selected
            if (selectedRow != null)
            {
                // Open update window to updata a game
                Update update = new Update(selectedRow);
                update.ShowDialog();

                FillDataGrid(); // Update datagrid with games
            }
            else
            {
                MessageBox.Show("No Game Selected To Update");
            }
        }

        // Remove game
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = dgGames.SelectedItem as DataRowView; // Get selected row

            // Check if a row has been selected
            if (selectedRow != null)
            {
                object idValue = selectedRow["id"]; // Get id of selected game

                if (idValue != null)
                {
                    string gameId = idValue.ToString();
                    
                    // Double check if you want to remove the game
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete game {gameId}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        // (Try to) Delete game
                        if (_gamesDB.DeleteGame(gameId))
                        {
                            MessageBox.Show($"Game {gameId} removed");
                        }
                        else
                        {
                            MessageBox.Show($"Failed to remove game {gameId}");
                        }

                        FillDataGrid(); // Update datagrid with games
                    }
                }
            }
            else
            {
                MessageBox.Show("No Game Selected To Remove");
            }

        }
    }
}
