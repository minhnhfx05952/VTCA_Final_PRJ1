using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using MySql.Data.MySqlClient;

namespace BL.Services
{
    internal class removeCVcs
    {
        MySqlConnection connection = DbContext.GetConnection();

        public void DeleteCVByUsername(string username)
        {
            try
            {
                connection.Open();

                // Kiểm tra xem người dùng có tồn tại
                if (!IsUserExists(username))
                {
                    Console.WriteLine("User does not exist.");
                    return;
                }

                // Kiểm tra xem người dùng có CV hay không
                if (!IsCVExists(username))
                {
                    Console.WriteLine("No CV found for this user.");
                    return;
                }

                // SQL query để xóa CV
                string deleteQuery = "DELETE FROM CV WHERE username = @username";
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@username", username);

                int rowsAffected = deleteCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("CV deleted successfully.");
                }
                else
                {
                    Console.WriteLine("CV could not be deleted.");
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private bool IsUserExists(string username)
        {
            // Kiểm tra xem người dùng có tồn tại hay không
            string query = "SELECT COUNT(*) FROM users WHERE username = @username";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            int count = Convert.ToInt32(command.ExecuteScalar());

            return count > 0;
        }

        private bool IsCVExists(string username)
        {
            // Kiểm tra xem có CV cho người dùng đã cho chưa
            string query = "SELECT COUNT(*) FROM CV WHERE username = @username";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            int count = Convert.ToInt32(command.ExecuteScalar());

            return count > 0;
        }
    }
}
