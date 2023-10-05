using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace EindProjectCSharp.Classes
{
    // Simon de Klerk
    internal class GamesDB
    {
        // Setup database connection
        MySqlConnection _connection = new MySqlConnection("Server=localhost;Database=games;Uid=root;Pwd=;");
        
        // Read
        public DataTable SelectGames()
        {
            DataTable result = new DataTable();
            try
            {
                _connection.Open(); // Open connection to database

                // Make sql command and get all games from database
                MySqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM games;";
                MySqlDataReader reader = cmd.ExecuteReader();
                result.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem SelectGames\n" + ex.Message);
            }
            finally
            {
                _connection.Close(); // Close connection to database
            }
            return result;
        }
        
        // Get studios to use as source for combobox in create new game file
        public List<string> SelectStudios()
        {
            List<string> studios = new List<string>();
            try
            {
                _connection.Open(); // Open connection to database

                // Make sql command and get all studio names from database
                MySqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = "SELECT name FROM studios;";
                MySqlDataReader reader = cmd.ExecuteReader();

                // Add all studios to list
                while (reader.Read())
                {
                    string studioName = reader["name"].ToString(); 
                    studios.Add(studioName);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem\n" + ex.Message);
            }
            finally
            {
                _connection.Close(); // Close connection to database
            }
            return studios;
        }


        // Get studio name with studio id
        public string GetStudioName(string studioId)
        {
            string studioName = null; // Initialize studioName
            try
            {
                _connection.Open(); // Open connection to database

                // Make sql command and get the studio name with the inputted id
                MySqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = $"SELECT name FROM studios WHERE id = @studioId;";
                cmd.Parameters.AddWithValue("@studioId", studioId);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    studioName = reader.GetString("name");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem GetStudioName\n" + ex.Message);
            }
            finally
            {
                _connection.Close(); // Close connection to database
            }
            return studioName;
        }

        // Get studio id with studio name
        public string GetStudioId(string studioName)
        {
            int studioId = -1; // Initialize studioId for later use
            try
            {
                _connection.Open(); // Open connection to database

                // Make sql command and get the studio id with the inputted name
                MySqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = $"SELECT id FROM studios WHERE name = @studioName;";
                cmd.Parameters.AddWithValue("@studioName", studioName);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    studioId = reader.GetInt32("id");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem GetStudioId\n" + ex.Message);
            }
            finally
            {
                _connection.Close(); // Close connection to database
            }
            return studioId.ToString();
        }

        // Create
        public bool InsertGame(string title, string description, string imagePath, string studioName)
        {
            bool succes = false;
            try
            {
                string studioId = GetStudioId(studioName);

                _connection.Open(); // Open connection to database

                // Make sql command and put a new game into the database (`id`, `title`, `description`, `imagePath`, `studioId`)
                MySqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = "INSERT INTO `games` (`id`, `title`, `description`, `imagePath`, `studioId`) VALUES (NULL, @title, @description, @imagePath, @studioId)";
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@imagePath", imagePath);
                cmd.Parameters.AddWithValue("@studioId", studioId);
                
                // Check if game has been inputted
                int nrOfRowsAffected = cmd.ExecuteNonQuery();
                succes = (nrOfRowsAffected != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem InsertGame\n" + ex.Message);
            }
            finally
            {
                _connection.Close(); // Close connection to database
            }
            return succes;
        }

        // Update
        public bool UpdateGame(string id, string title, string description, string imagePath, string studioName)
        {
            bool succes = false;
            try
            {
                string studioId = GetStudioId(studioName);

                _connection.Open(); // Open connection to database

                // Make sql command and update the game with inputted game id (`id`, `title`, `description`, `imagePath`, `studioId`)
                MySqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = "UPDATE `games` SET `title` = @title, `description` = @description, `imagePath` = @imagePath, `studioId` = @studioId WHERE `games`.`id` = @id; ";
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@imagePath", imagePath);
                cmd.Parameters.AddWithValue("@studioId", studioId);
                cmd.Parameters.AddWithValue("@id", id);

                // Check if game has been updated
                int nrOfRowsAffected = cmd.ExecuteNonQuery();
                succes = (nrOfRowsAffected != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem UpdateGame\n" + ex.Message);
            }
            finally
            {
                _connection.Close(); // Close connection to database
            }
            return succes;
        }

        // Delete
        public bool DeleteGame(string gameId)
        {
            bool succes = false;
            try
            {
                _connection.Open(); // Open connection to database

                // Make sql command and delete game with inputted game id
                MySqlCommand command = _connection.CreateCommand();
                command.CommandText = "DELETE FROM games WHERE id = @id;";
                command.Parameters.AddWithValue("@id", gameId);

                // Check if game has been deleted
                int nrOfRowsAffected = command.ExecuteNonQuery();
                succes = (nrOfRowsAffected != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem DeleteGame\n" + ex.Message);
            }
            finally
            {
                _connection.Close(); // Close connection to database
            }
            return succes;
        }
    }
}