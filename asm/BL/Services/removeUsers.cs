using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using MySql.Data.MySqlClient;

namespace BL.Services
{
    internal class removeUsers
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
        public void DeleteUser(string username)
        {
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            try
            {
                connection.Open();

                // Kiểm tra xem người dùng tồn tại
                if (!IsUserExists(username))
                {
                    connection.Open();
                    Console.WriteLine("User does not exist.");
                    return;
                }
                connection.Open();
                // SQL query để xóa người dùng
                string deleteQuery = "DELETE FROM users WHERE username = @username"+"DELETE FROM departments  WHERE username = @username";
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@username", username);

                int rowsAffected = deleteCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("User deleted successfully.");
                }
                else
                {
                    Console.WriteLine("User could not be deleted.");
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
