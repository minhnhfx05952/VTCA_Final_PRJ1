using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using MySql.Data.MySqlClient;

namespace BL.Services
{
    internal class view1mem
    {
        MySqlConnection connection = DbContext.GetConnection();
        private bool IsUserExists(string username)
        {
            try
            {
                // Kiểm tra xem người dùng có tồn tại hay không
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
           public void ViewUserInfoByUsername(string username)
           {
               Console.WriteLine("╔═══════════════════════════════════════════╗");
               try
               {
                   connection.Open();

                   // Kiểm tra xem người dùng có tồn tại
                   if (!IsUserExists(username))
                   {
                       connection.Open();
                       Console.WriteLine("User does not exist.");
                       connection.Close();
                       return;
                   }
                   connection.Close();
                   connection.Open();
                   // SQL query để lấy thông tin người dùng
                   string selectQuery = "SELECT * FROM users WHERE username = @username";
                string query = "SELECT u.id, u.username, u.password, u.email, u.role, u.permission, d.department_name " +
           "FROM users u " +
           "JOIN employee_departments ed ON u.username = ed.employee_username " +
           "JOIN departments d ON ed.department_id = d.department_id " +
           "WHERE u.role = 'user'";


                MySqlCommand selectCommand = new MySqlCommand(query, connection);
                selectCommand.Parameters.AddWithValue("@username", username);

                MySqlDataReader reader = selectCommand.ExecuteReader();

                   if (reader.Read())
                   {
                       Console.WriteLine("User Information:");
                       Console.WriteLine("Username: " + reader["username"]);
                       Console.WriteLine("Password: " + reader["password"]);
                       Console.WriteLine("Email: " + reader["email"]);
                       Console.WriteLine("Role: " + reader["role"]);
                       Console.WriteLine("Permission: " + reader["permission"]);
                       Console.WriteLine("department name: " + reader["department_name"]);
                }
                   else
                   {
                       Console.WriteLine("No information found for this user.");
                   }

                   reader.Close();
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
