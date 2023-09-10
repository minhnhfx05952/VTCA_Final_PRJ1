using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DAL;

namespace BL.Services
{
    internal class ViewMemInfo
    {
        private MySqlConnection connection;

        public ViewMemInfo()
        {
            connection = DbContext.GetConnection();
        }

        public void ViewEmployeesWithDepartments()
        {
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            try
            {
                connection.Open();

                string query = "SELECT u.id, u.username, u.password, u.email, u.role, u.permission, d.department_name " +
                               "FROM users u " +
                               "JOIN employee_departments ed ON u.username = ed.employee_username " +
                               "JOIN departments d ON ed.department_id = d.department_id " +
                               "WHERE u.role = 'user'";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int userId = reader.GetInt32("id");
                            string username = reader.GetString("username");
                            string password = reader.GetString("password");
                            string email = reader.GetString("email");
                            string role = reader.GetString("role");
                            string permission = reader.GetString("permission");
                            string department = reader.GetString("department_name");

                            Console.WriteLine($"User ID: {userId}");
                            Console.WriteLine($"Username: {username}");
                            Console.WriteLine($"Password: {password}");
                            Console.WriteLine($"Email: {email}");
                            Console.WriteLine($"Role: {role}");
                            Console.WriteLine($"Permission: {permission}");
                            Console.WriteLine($"Department: {department}");
                            Console.WriteLine();
                        }
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
            Console.WriteLine("╚═══════════════════════════════════════════╝");
        }

    }
}
