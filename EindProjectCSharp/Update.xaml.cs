using EindProjectCSharp.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Formats.Asn1;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        GamesDB _gamesDB = new GamesDB();
        
        string gameId = null;

        public Update(DataRowView game)
        {
            InitializeComponent();
            FillStudioSelection();
            FillScreen(game);
            gameId = game["id"].ToString();
        }

        private void FillStudioSelection()
        {
            cmbStudio.ItemsSource = _gamesDB.SelectStudios();
        }
        private void FillScreen(DataRowView game)
        {
            tbTitle.Text = game["title"].ToString();
            tbDescription.Text = game["description"].ToString();
            tbImagePath.Text = game["imagePath"].ToString();
            
            object studioId = game["studioId"];
            string studioName = _gamesDB.GetStudioName(studioId.ToString());

            foreach (string item in cmbStudio.Items)
            {
                if (item == studioName)
                {
                    cmbStudio.SelectedItem = item;
                    break; // Exit the loop once a match is found.
                }
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {

            if (_gamesDB.UpdateGame(gameId, tbTitle.Text, tbDescription.Text, tbImagePath.Text, cmbStudio.Text))
            {
                MessageBox.Show($"Game Update");
            }
            else
            {
                MessageBox.Show($"Game Update Failed");
            }
            this.Close();
        }
    }
}
