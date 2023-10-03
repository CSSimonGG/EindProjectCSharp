

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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GamesDB _gamesDB = new GamesDB();
        public MainWindow()
        {
            InitializeComponent();
            FillDataGrid();
        }
        private void FillDataGrid()
        {
            DataTable students = _gamesDB.SelectGames();
            if (students != null)
            {
                dgGames.ItemsSource = students.DefaultView;
            }
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.ShowDialog();
            FillDataGrid();
        }
    }
}
