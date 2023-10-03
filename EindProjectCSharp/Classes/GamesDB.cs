using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace EindProjectCSharp.Classes
{
    internal class GamesDB
    {
        MySqlConnection _connection = new MySqlConnection("Server=localhost;Database=games;Uid=root;Pwd=;");
        
        // Read
        public DataTable SelectGames()
        {
            DataTable result = new DataTable();
            try
            {
                _connection.Open();
                MySqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM games;";
                MySqlDataReader reader = cmd.ExecuteReader();
                result.Load(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem SelectGames\n" + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public List<string> SelectStudios()
        {
            List<string> studios = new List<string>();
            try
            {
                _connection.Open();

                MySqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = "SELECT name FROM studios;";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string studioID = reader["name"].ToString(); 
                    studios.Add(studioID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem\n" + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return studios;
        }

        // Create
        public bool InsertGame(string title, string description, string imagePath, string studioName)
        {
            bool succes = false;
            try
            {
                _connection.Open();

                MySqlCommand cmd = _connection.CreateCommand();

                cmd.CommandText = $"SELECT id FROM studios WHERE name = @studioName;";
                cmd.Parameters.AddWithValue("@studioName", studioName);
                MySqlDataReader reader = cmd.ExecuteReader();

                int studioId = -1; // Initialize studioId 
                if (reader.Read())
                {
                    studioId = reader.GetInt32("id");
                }
                reader.Close();

                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO `games` (`id`, `title`, `description`, `imagePath`, `studioId`) VALUES (NULL, @title, @description, @imagePath, @studioId)";
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@imagePath", imagePath);
                cmd.Parameters.AddWithValue("@studioId", studioId);
                
                int nrOfRowsAffected = cmd.ExecuteNonQuery();
                succes = (nrOfRowsAffected != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem InsertGame\n" + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return succes;
        }

        // Update


        // Delete
        public bool DeleteGame(string gameId)
        {
            bool succes = false;
            try
            {
                _connection.Open();
                MySqlCommand command = _connection.CreateCommand();
                command.CommandText = "DELETE FROM games WHERE id = @id;";
                command.Parameters.AddWithValue("@id", gameId);
                int nrOfRowsAffected = command.ExecuteNonQuery();
                succes = (nrOfRowsAffected != 0);
            }
            catch (Exception)
            {
                //Problem with the database
            }
            finally
            {
                _connection.Close();
            }
            return succes;
        }
    }
}