using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using MySql.Data.MySqlClient;

namespace BL.Services
{
    internal class viewCV1
    {
        MySqlConnection connection = DbContext.GetConnection();

        public void ViewCVByUsername(string username)
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

                // SQL query để lấy đường dẫn tệp CV
                string selectQuery = "SELECT cv_file_path FROM CV WHERE username = @username";
                MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@username", username);

                string cvFilePath = selectCommand.ExecuteScalar() as string;

                if (!string.IsNullOrEmpty(cvFilePath))
                {
                    Console.WriteLine("CV File Path: " + cvFilePath);
                    // Đọc hoặc hiển thị nội dung của tệp CV tại đây nếu cần
                }
                else
                {
                    Console.WriteLine("CV not found for this user.");
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

    }
}
