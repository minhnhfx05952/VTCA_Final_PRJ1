using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BL.Services
{
    internal class addUsers
    {
        MySqlConnection connection = DbContext.GetConnection();
        //AddUserToDatabase()
        public bool IsUsernameExists(string username)
        {
            try
            {
                
                string query = "SELECT COUNT(*) FROM users WHERE username = @username";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                int count = Convert.ToInt32(command.ExecuteScalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false; // Trả về false nếu xảy ra lỗi
            }
            finally
            {
                connection.Close();
            }
        }
        public void AddUserToDatabase()
        {
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            try
            {
                connection.Open();
                Console.WriteLine("Enter user details:");
                Console.Write("Username: ");
                string username = Console.ReadLine(); 
                while (IsUsernameExists(username))
                {
                    connection.Open();
                    Console.WriteLine("Username already exists. Please try again.");
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                 }
                connection.Open();
                Console.Write("Password: ");
                string pw = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("role: ");
                string role = Console.ReadLine();
                Console.Write("Permission: ");
                string permission = Console.ReadLine();
                

                // You can add more fields as needed

                // SQL query to insert the user into the database
                //string insertQuery = "INSERT INTO users (username, email) VALUES (@username, @email)";
                //insert usernam, pw, role and permission
                string insertQuery = "INSERT INTO users (username, password, email, role, permission) VALUES (@username, @pw, @email, @role, @permission)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);

                // Parameters to prevent SQL injection
                insertCommand.Parameters.AddWithValue("@username", username);
                insertCommand.Parameters.AddWithValue("@pw", pw);
                insertCommand.Parameters.AddWithValue("@role", role);
                insertCommand.Parameters.AddWithValue("@permission", permission);
                insertCommand.Parameters.AddWithValue("@email", email);

                // Execute the SQL query to insert the user
                int rowsAffected = insertCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("User added successfully.");
                }
                else
                {
                    Console.WriteLine("User could not be added.");
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            Console.WriteLine("╚═══════════════════════════════════════════╝");
        }

    }
}
