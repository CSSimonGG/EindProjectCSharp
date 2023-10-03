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
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        GamesDB _gamesDB = new GamesDB();
          
        public Profile()
        {
            InitializeComponent();
            FillDataGrid();
        }
        private void FillDataGrid()
        {
            DataTable games = _gamesDB.SelectGames();
            if (games != null)
            {
                dgGames.ItemsSource = games.DefaultView;
            }
        }


        // Create new game
        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Create create = new Create();
            create.ShowDialog();
        }

        // Update game
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update update = new Update();
            update.ShowDialog();
        }

        // Remove game
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = dgGames.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                object idValue = selectedRow["id"];

                if (idValue != null)
                {
                    string gameId = idValue.ToString();
                    
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete game {gameId}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (_gamesDB.DeleteGame(gameId))
                        {
                            MessageBox.Show($"Game {gameId} removed");
                        }
                        else
                        {
                            MessageBox.Show($"Failed to remove game {gameId}");
                        }

                        FillDataGrid();
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
