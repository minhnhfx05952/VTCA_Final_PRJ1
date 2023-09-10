using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL.Services
{
    internal class modifyUser
    {
        MySqlConnection connection = DbContext.GetConnection();

        public void ModifyUserInformation(string username)
        {
            try
            {
                connection.Open();

                Console.WriteLine("Enter new user details:");
                Console.Write("Password: ");
                string newPassword = Console.ReadLine();
                Console.Write("Email: ");
                string newEmail = Console.ReadLine();
                Console.Write("Role: ");
                string newRole = Console.ReadLine();
                Console.Write("Permission: ");
                string newPermission = Console.ReadLine();

                // SQL query to update user information
                string updateQuery = "UPDATE users SET password = @newPassword, email = @newEmail, role = @newRole, permission = @newPermission WHERE username = @username";
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@newPassword", newPassword);
                updateCommand.Parameters.AddWithValue("@newEmail", newEmail);
                updateCommand.Parameters.AddWithValue("@newRole", newRole);
                updateCommand.Parameters.AddWithValue("@newPermission", newPermission);
                updateCommand.Parameters.AddWithValue("@username", username);

                int rowsAffected = updateCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("User information updated successfully.");
                }
                else
                {
                    Console.WriteLine("User information could not be updated.");
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

    }
}
