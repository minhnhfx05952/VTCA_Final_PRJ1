using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using MySql.Data.MySqlClient;

namespace BL.Services
{
    internal class cvManage
    {

        public bool CheckIfUsernameExistsInCV(string username)
        {
            MySqlConnection connection = DbContext.GetConnection();
            bool usernameExistsInCV = false;

            try
            {
                connection.Open();

                // SQL query to check if the username exists in the CV table
                string query = "SELECT COUNT(*) FROM CV WHERE username = @username";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count > 0)
                {
                    // The username exists in the CV table
                    usernameExistsInCV = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return usernameExistsInCV;
        }
        public void FindCVByUsername(string username)
        {
            MySqlConnection connection = DbContext.GetConnection();
            // Find CV by username
            try
            {
                connection.Open();

                // SQL query to find CVs for a specific username
                string query = "SELECT * FROM CV WHERE username = @username";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("╔═══════════════════════════════════════════╗");
                        Console.WriteLine($"CVs for Username: {username}");
                        Console.WriteLine("╠═══════════════════════════════════════════╣");

                        do
                        {
                            int cvId = reader.GetInt32("cv_id");
                            string cvName = reader.GetString("cv_name");
                            string filePath = reader.GetString("cv_file_path");

                            Console.WriteLine($"CV ID: {cvId}");
                            Console.WriteLine($"CV Name: {cvName}");
                            Console.WriteLine($"File Path: {filePath}");
                            Console.WriteLine("╠═══════════════════════════════════════════╣");
                        } while (reader.Read());

                        Console.WriteLine("╚═══════════════════════════════════════════╝");
                    }
                    else
                    {
                        Console.WriteLine($"No CVs found for Username: {username}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
